<template>
  <v-app id="inspire" :style="{ background: '#C8C8C1' }">
    <NavigationDrawer />

    <v-main>
      <v-container fluid fill-height class="pa-2-md-5">
        <v-icon size="36" class="d-none d-md-inline-block">mdi mdi-invoice-text-multiple-outline</v-icon>
        <v-icon size="24" class="d-inline-block d-md-none">mdi mdi-invoice-text-multiple-outline</v-icon>
        <h1>Histórico de Condicionais</h1>
        <br />

        <!-- ALERTAS -->
        <v-alert v-if="successMessage" type="success" dismissible>
          {{ successMessage }}
        </v-alert>

        <v-alert v-if="errorMessage" type="error" dismissible>
          {{ errorMessage }}
        </v-alert>

        <v-sheet class="mx-auto" width="auto" style="overflow-x: auto;">
          <v-data-table
            :style="{ background: '#8e8d8d' }"
            :headers="headers"
            :items="orders"
            item-value="id"
            :search="search"
            :sort-by="[{ key: 'id', order: 'asc' }]"
            class="elevation-1"
          >

          <template v-slot:top>
            <v-toolbar
              flat
              color="#545454"
            >
              <v-toolbar-title>Buscar Condicional por Status</v-toolbar-title>
              <v-divider
                class="mx-4"
                inset
                vertical
              ></v-divider>
              <v-spacer>
                <v-responsive
                  class="mx-auto"
                  max-width="555"
                >
                  <v-text-field
                    v-model="search"
                    label="Status"
                    prepend-inner-icon="mdi-magnify"
                    variant="outlined"
                    hide-details
                    single-line
                    rounded
                    :style="{ maxWidth: '400px' }"
                  ></v-text-field>
                </v-responsive>
              </v-spacer>
              <v-dialog
                v-model="dialog"
                max-width="500px"
              ></v-dialog>
            </v-toolbar>
          </template>

            <!-- Exibe os itens dentro de `listOrderItem` -->
            <template v-slot:item.listOrderItem="{ item }">
              <div class="lista-de-produtos">
                <v-list dense class="lista-de-produtos">
                  <v-list-item v-for="(orderItem, index) in item.items" :key="index">
                    <v-list-item-title>{{ orderItem.productId || 'ID do produto não disponível!' }}</v-list-item-title>
                    <v-list-item-subtitle>
                      Variação: {{ orderItem.variation }}, Status: {{ convertProductStatus(orderItem.status) }}
                    </v-list-item-subtitle>
                  </v-list-item>
                </v-list>
              </div>
            </template>

            <template v-slot:item.status="{ item }">
              <span v-bind:class="{
                'status-pendente': item.status === 'Pendente',
                'status-aguardando': item.status === 'Aguardando encerramento',
                'status-fechado': item.status === 'Fechado'
              }">
                {{ item.status }}
              </span>
            </template>

            <template v-slot:item.closingDate="{ item }">
              {{ formatDate(item.closingDate) || 'Condicional ainda em aberto' }}
            </template>
            <template v-slot:item.creationDate="{ item }">
              {{ formatDate(item.creationDate) }}
            </template>
            <template v-slot:item.actions="{ item }">
              <div class="actions-container">
                <v-btn @click="editOrder(item)" color="primary" class="ma-1" icon="mdi-pencil"></v-btn>
                <v-btn @click="deleteOrder(item)" color="#E53935" class="ma-1" icon="mdi-delete"></v-btn>
                <v-btn @click="closeOrder(item)" color="green" class="ma-1" icon="mdi-lock-outline"></v-btn>
              </div>
            </template>

          </v-data-table>
        </v-sheet>

        <v-dialog v-model="dialog" max-width="600px">
          <v-card>
            <v-card-title>Editar Condicional</v-card-title>
            <v-card-text>
              <v-form ref="form">
                <v-select disabled="" v-model="editedOrder.status" :items="statuses" label="Status da Condicional"></v-select>

                <v-divider></v-divider>
                <v-list>
                  <v-list-item v-for="(item, index) in editedOrder.items" :key="index">
                    <v-text-field v-model="item.productId" label="ID do produto"></v-text-field>
                    <v-text-field v-model="item.variation" label="Variação do Produto"></v-text-field>
                    <v-select v-model="item.status" :items="itemStatuses" label="Estado do Produto"></v-select>
                  </v-list-item>
                </v-list>
              </v-form>
            </v-card-text>
            <v-card-actions>
              <v-btn color="primary" @click="saveOrder">Salvar</v-btn>
              <v-btn @click="dialog = false">Cancelar</v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>
      </v-container>
    </v-main>
  </v-app>
</template>

<script>
import axios from 'axios';

export default {
  data() {
    return {
      orders: [],
      dialog: false,
      editedOrder: {},
      successMessage: '',
      errorMessage: '',
      statuses: ['Pendente', 'Aguardando encerramento', 'Fechado'],
      itemStatuses: ['Pendente', 'Devolvido', 'Comprado'],
      headers: [
        { title: 'Id', align: 'start', sortable: true, value: 'id' },
        { title: 'Nome do Cliente', value: 'customerName' },
        { title: 'Data de Criação', value: 'creationDate' },
        { title: 'Data de Fechamento', value: 'closingDate' },
        { title: 'Observação', value: 'observation' },
        { title: 'Lista de Produtos', value: 'listOrderItem' },
        { title: 'Status', value: 'status' },
        { title: 'Ações', value: 'actions', sortable: false }
      ],
      fetchTimeout: null,
      search: '',
      isRateLimited: false,
    };
  },

  methods: {
    async fetchOrders() {
      // Evita múltiplas tentativas durante erro 429
      if (this.isRateLimited) return;

      clearTimeout(this.fetchTimeout);
      this.fetchTimeout = setTimeout(async () => {
        try {
          const response = await axios.get('http://localhost:3002/api/Order');
          this.orders = (response.data.result || []).map(order => ({
            id: order.id,
            customerName: `${order.customer.firstName} ${order.customer.lastName}`,
            listOrder: order.customer.listOrder,
            creationDate: order.creationDate,
            status: this.convertOrderStatus(order.status),
            closingDate: order.closingDate,
            observation: order.observation,
            items: order.listOrderItem.map(item => ({
              id: item.id,
              productId: item.productId,
              variation: item.variation,
              status: item.status
            })),
          }));
          this.isRateLimited = false; // Resetar flag após sucesso
        } catch (error) {
          if (error.response && error.response.status === 429) {
            console.warn("Muitas requisições. Tentando novamente em breve.");
            this.isRateLimited = true; // Set flag para limitar chamadas
            setTimeout(() => {
              this.isRateLimited = false; // Desbloquea tentativas após 10 segundos
              this.fetchOrders();
            }, 10000); // Tenta novamente após 10 segundos
          } else {
            console.error("Erro ao buscar condicional:", error);
            this.errorMessage = "Erro ao buscar as condicionais.";
          }
        }
      }, 1000);
    },
    

    editOrder(item) {
      this.editedOrder = { ...item };
      this.dialog = true;
    },

    async saveOrder() {
      try {
        // Inclua o campo observation diretamente no payload e defina o status corretamente.
        const payload = {
          status: this.convertOrderStatusToNumber(this.editedOrder.status), // Inclua o status atual
          listCreatedItem: this.editedOrder.items
            .filter(item => !item.id)
            .map(item => ({
              productId: item.productId,
              variation: item.variation
            })),
          listUpdatedItem: this.editedOrder.items
            .filter(item => item.id)
            .map(item => ({
              id: item.id,
              inputUpdate: {
                variation: item.variation,
                status: this.convertProductStatusToNumber(item.status)
              }
            })),
          listDeletedItem: this.editedOrder.itemToDelete || []
        };

        await axios.put(`http://localhost:3002/api/Order/${this.editedOrder.id}`, payload);

        this.dialog = false;
        this.fetchOrders(); // Atualiza a lista de pedidos
        this.successMessage = 'Condicional atualizada com sucesso!';
      } catch (error) {
        console.error('Erro ao salvar condicional:', error);
        this.errorMessage = 'Erro ao salvar a condicional.';
      }
    },



    async closeOrder(item) {
      try {
        // Faz a chamada PUT para fechar a condicional
        await axios.put(`http://localhost:3002/api/Order/Close/${item.id}`);
        this.successMessage = 'Pedido fechado com sucesso!';
        this.fetchOrders(); // Atualiza a lista de condicionais após o fechamento
      } catch (error) {
        console.error('Erro ao fechar a condicional:', error);
        this.errorMessage = 'Erro ao fechar ao fechar a condicional.';
      }
    },

    async deleteOrder(item) {
      try {
        await axios.delete(`http://localhost:3002/api/Order/${item.id}`);
        this.successMessage = 'Condicional excluída com sucesso!';
        this.fetchOrders();
      } catch (error) {
        console.error('Erro ao excluir condicional:', error);
        this.errorMessage = 'Erro ao excluir a condicional.';
      }
    },

    convertOrderStatus(status) {
      const statusMap = {
        0: 'Pendente',
        1: 'Aguardando encerramento',
        2: 'Fechado'
      };
      return statusMap[status] || 'Pendente';
    },

    convertProductStatus(status) {
      const statusMap = {
        0: 'Pendente',
        1: 'Devolvido',
        2: 'Comprado'
      };
      return statusMap[status] || 'Pendente';
    },

    convertProductStatusToNumber(status) {
      const statusMap = {
        'Pendente': 0,
        'Devolvido': 1,
        'Comprado': 2
      };
      return statusMap[status] || 0;
    },

    convertOrderStatusToNumber(status) {
      const statusMap = {
        'Pendente': 0,
        'Aguardando encerramento': 1,
        'Fechado': 2
      };
      return statusMap[status] || 0;
    },

    formatDate(date) {
      if (!date) return '';
      const options = { year: 'numeric', month: '2-digit', day: '2-digit' };
      return new Date(date).toLocaleDateString('pt-BR', options);
    },


    determineOrderStatus(allItemsStatus) {
      if (allItemsStatus.includes('Pendente')) return 'Pendente';
      if (allItemsStatus.includes('Devolvido')) return 'Aguardando encerramento';
      return 'Fechado';
    }
  },
  created() {
    // Adiciona debounce(garante que o código seja disparado apenas uma vez por cada entrada do usuário) para a primeira chamada
    this.fetchTimeout = setTimeout(() => {
      this.fetchOrders();
    }, 1000);
  }
};
</script>


<style scoped>
.lista-de-produtos {
  background-color: #8e8d8d;
}

.status-pendente {
  color: rgb(160, 13, 13); /* cor para Pendentes */
}

.status-aguardando {
  color: rgb(234, 157, 14); /* cor para Aguardando encerramento */
}

.status-fechado {
  color: rgb(1, 125, 1); /* cor para Fechado */
}

.actions-container {
  display: flex;
  gap: 8px; /* Ajusta o espaçamento entre os botões */
}

.v-btn {
  background-color: transparent; 
  box-shadow: none; 
}

.v-btn .v-icon {
  color: rgb(240, 238, 238); 
}
</style>
