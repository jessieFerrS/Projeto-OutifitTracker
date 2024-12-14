<template>
    <v-app
      id="inspire"
      :style="{ background: '#C8C8C1' }">
      <NavigationDrawer/>
     
      <v-main>
        <v-container fluid fill-height class="pa-2-md-5">
          <v-icon size="36" class="d-none d-md-inline-block">mdi mdi-tshirt-crew</v-icon>
          <v-icon size="24" class="d-inline-block d-md-none">mdi mdi-tshirt-crew</v-icon>
          <h1>Lista de Produtos</h1>
          <br>

          <!-- ALERTAS -->
          <v-alert
            v-if="successMessage"
            type="success"
            dismissible
          >
            {{ successMessage }}
          </v-alert>

          <v-alert
            v-if="errorMessage"
            type="error"
            dismissible
          >
            {{ errorMessage }}
          </v-alert>

          <v-sheet class="mx-auto" width="auto" style="overflow-x: auto;">
              <v-data-table :style="{background: '#8e8d8d'}"
                :headers="headers"
                :items="products"
                :search="search"
                :sort-by="[{ key: 'id', order: 'asc' }]"
              >
              <template v-slot:item.price="{ item }">
                {{ formatCurrency(item.price) }}
              </template>

                <template v-slot:top>
                  <v-toolbar
                    flat
                    color="#545454"
                  >
                    <v-toolbar-title>Buscar Produtos por Categoria</v-toolbar-title>
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
                          label="Categoria"
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
                    >
                      <v-card>
                        <v-card-title>
                          <span class="text-h5">{{ formTitle }}</span>
                        </v-card-title>

                        <v-card-text>
                          <v-container>
                            <v-row>
                              <!-- Não editáveis
                              <v-col
                                cols="12"
                                md="4"
                                sm="6"
                              >
                                <v-text-field
                                  v-model="editedItem.code"
                                  label="Código"
                                ></v-text-field>
                              </v-col>-->
                              <v-col
                                cols="12"
                                md="4"
                                sm="6"
                              >
                                <v-text-field
                                  v-model="editedItem.description"
                                  label="Descrição"
                                ></v-text-field>
                              </v-col>
                              
                              <v-col
                                cols="12"
                                md="4"
                                sm="6"
                              >
                                <v-text-field
                                  v-model="editedItem.price"
                                  label="Preço Unitário"
                                ></v-text-field>
                              </v-col>

                              <!-- Não editavel
                              <v-col
                                cols="12"
                                md="4"
                                sm="6"
                              >
                                <v-text-field
                                  v-model="editedItem.brand"
                                  label="Marca"
                                ></v-text-field>
                              </v-col>-->
                              <v-col
                                cols="12"
                                md="4"
                                sm="6"
                              >
                                <v-text-field
                                  v-model="editedItem.category"
                                  label="Categoria"
                                ></v-text-field>
                              </v-col>
                            </v-row>
                          </v-container>
                        </v-card-text>

                        <v-card-actions>
                          <v-spacer></v-spacer>
                          <v-btn
                            color="blue-darken-1"
                            variant="text"
                            @click="close"
                          >
                            Cancelar
                          </v-btn>
                          <v-btn
                            color="blue-darken-1"
                            variant="text"
                            @click="save"
                          >
                            Salvar
                          </v-btn>
                        </v-card-actions>
                      </v-card>
                    </v-dialog>
                    <v-dialog v-model="dialogDelete" max-width="600px">
                      <v-card>
                        <v-card-title
                        class="text-h5"
                        ><br>Você tem certeza que deseja excluir esse produto?</v-card-title>
                        <v-card-actions>
                          <v-spacer></v-spacer>
                          <v-btn color="blue-darken-1" variant="text" @click="closeDelete">Cancelar</v-btn>
                          <v-btn color="red" variant="text" @click="deleteItemConfirm">Excluir</v-btn>
                          <v-spacer></v-spacer>
                        </v-card-actions>
                      </v-card>
                    </v-dialog>
                  </v-toolbar>
                </template>
                <template v-slot:item.actions="{ item }">
                  <v-icon
                    class="me-2"
                    size="small"
                    @click="editItem(item)"
                  >
                    mdi-pencil
                  </v-icon>
                  <v-icon
                    size="small"
                    @click="deleteItem(item)"
                  >
                    mdi-delete
                  </v-icon>
                </template>
                <template v-slot:no-data>
                  <v-btn
                    color="black"
                    @click="initialize"
                  >
                    Reset
                  </v-btn>
                </template>
              </v-data-table>
          </v-sheet>
        </v-container>
      </v-main>
    </v-app>
   
</template>
<script setup>
  import NavigationDrawer from "@/components/NavigationDrawer.vue";
  import { mask } from 'vue-the-mask';
  </script>
 
<script>
import api from '../services/api';
export default {
  directives: {mask},
  data: () => ({
      search: '',
      dialog: false,
      dialogDelete: false,
      headers: [
        { title: 'Id', align: 'start', sortable: true, value: 'id'},
        { title: 'Código', sortable: false, value: 'code'},
        { title: 'Descrição', value: 'description', sortable: false},
        { title: 'Preço Unitário', value:'price', sortable: false },
        { title: 'Marca', value: 'brand', sortable: false },
        { title: 'Categoria', value: 'category', sortable: false},
        { title: 'Ações', value: 'actions', sortable: false },
      ],
      products: [],
      editedIndex: -1,
      editedItem: {
        id: '',
        code: '',
        description: '',
        price: '',
        brand: '',
        category: '',
      },
      defaultItem: {
        id: '',
        code: '',
        description: '',
        price: '',
        brand: '',
        category: '',
      },

      successMessage: '',
      errorMessage: '',

    }),
 
    computed: {
      formTitle () {
        return this.editedIndex === -1 ? "Editar item" : "Editar item"
      },
    },
     
 
    watch: {
      dialog (val) {
        val || this.close()
      },
      dialogDelete (val) {
        val || this.closeDelete()
      },
    },
 
    created () {
      this.getProduct()
    },
 
    methods: {
      getProduct(){
        api
          .get('http://localhost:3002/api/Product')
          .then((res) => {
            console.log(res.data.result);
            this.products = res.data.result || [];
            console.log(this.products);
          })
          .catch((error) => {
            console.log(error);
          })
      },

      initialize () {
        this.getProduct();
      },
 
      editItem (item) {
        this.editedIndex = this.products.indexOf(item)
        this.editedItem = Object.assign({}, item)
        this.dialog = true
      },
 
      deleteItem (item) {
        this.editedIndex = this.products.indexOf(item)
        this.editedItem = Object.assign({}, item)
        this.dialogDelete = true
      },
 
      deleteItemConfirm () {
        if (this.editedItem.id) {
          api
            .delete(`http://localhost:3002/api/Product/${this.editedItem.id}`)
            .then(() => {
              this.products.splice(this.editedIndex, 1);
              this.successMessage = 'Produto Excluído com Sucesso!';
              this.closeDelete();
            })
        }
      },
 
      close () {
        this.dialog = false
        this.$nextTick(() => {
          this.editedItem = Object.assign({}, this.defaultItem)
          this.editedIndex = -1
        })
      },
 
      closeDelete () {
        this.dialogDelete = false
        this.$nextTick(() => {
          this.editedItem = Object.assign({}, this.defaultItem)
          this.editedIndex = -1
        })
      },
 
      save () {
    this.editedItem.price = parseFloat(this.editedItem.price);

    if (this.editedIndex >= 0) {
        // Atualizar um produto existente
        api
            .put(`http://localhost:3002/api/Product/${this.editedItem.id}`, this.editedItem)
            .then(() => {
                Object.assign(this.products[this.editedIndex], this.editedItem);
                this.successMessage = 'Produto Editado com Sucesso!';
                this.close();
            })
            .catch((error) => {
                // Exibe a mensagem de erro personalizada do backend
                this.errorMessage = error.response.data.errorMessage || 'Erro ao Tentar Editar o Produto.';
                console.error(error);
            });
    } else {
        // Criar um novo produto
        api
            .post('http://localhost:3002/api/Product', this.editedItem)
            .then((res) => {
                this.products.push(res.data);
                this.close();
            })
            .catch((error) => {
                // Exibe a mensagem de erro personalizada do backend
                this.errorMessage = error.response.data.errorMessage || 'Erro ao Criar o Produto. Código Duplicado!';
                console.error(error);
            });
    }
  },

      //FORMATA VALORES MONETÁRIOS
      formatCurrency(value) {
        if (value == null) return '';
        return value.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
      }
    },
}
</script>
 
<style scoped>
h1 {
  font-family: 'Montserrat';
}

 
</style>