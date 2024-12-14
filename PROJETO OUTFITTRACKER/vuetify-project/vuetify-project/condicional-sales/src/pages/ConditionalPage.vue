<template>
    <v-app 
        id="inspire"
        :style="{ background: '#C8C8C1' }"
        >
        <NavigationDrawer/>

        <v-main>
            <v-container fluid fill-height >
                <v-row>
                    <v-col cols="12">
                        <v-icon icon="mdi mdi-file-document-plus-outline"></v-icon>  
                        <h1>Nova Condicional</h1>
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

                    </v-col>
                    <v-col>
                        <v-row>
                            <v-col 
                                cols="4"
                            >
                                <v-text-field 
                                v-model="clientId"
                                label="ID Cliente" 
                                variant='outlined'
                                @change="fetchClientDetails"
                                @input="clearErrorMessage"
                                ></v-text-field>
                            </v-col>
                            <v-col 
                                cols="8"    
                            >
                                <v-text-field 
                                v-model="client.name"
                                label="Nome Cliente" 
                                variant='outlined'
                                ></v-text-field>
                            </v-col>
                            <v-col cols="4">
                                <v-select 
                                v-model="status"
                                label="Status" 
                                :items="statusOptions" 
                                variant='outlined'
                                ></v-select>
                            </v-col>
                            <v-col 
                                cols="4" 
                            >
                                <v-text-field 
                                v-model="closingDate"
                                label="Data Fechamento" variant='outlined'
                                type="date"
                                ></v-text-field>
                            </v-col>
                            
                            <v-col cols="4">
                                <v-text-field 
                                v-model="client.cpf"
                                label="CPF" 
                                variant='outlined'></v-text-field>
                            </v-col>

                            <v-col cols="12">
                                <v-text-field 
                                    v-model="observation" 
                                    label="Observação" 
                                    variant="outlined" 
                                    dense
                                ></v-text-field>
                            </v-col>

                            <!-- Tabela com os Produtos Pedidos-->
                            <v-col cols="12">
                                <v-sheet class="d-flex">
                                    <v-data-table-virtual
                                        :headers="headers" 
                                        :items="items"
                                        item-value="code" 
                                        v-model="selectedItems"
                                        show-select
                                        class="mx-auto custom-data-table"
                                        height="510"
                                        
                                    >
                                    </v-data-table-virtual>
                                </v-sheet>
                            </v-col>
                        </v-row>    
                    </v-col>
                    
                     <!--- Menu de ações -->
                     <v-col :style="{ margin: auto, width: auto}">
                        <v-text-field 
                            v-model="productId" 
                            label="ID Produto" 
                            variant= outlined 
                            dense 
                            @change="fetchProductDetails"
                            @input="clearErrorMessage"
                        ></v-text-field>
                        <v-text-field 
                            v-model="editedItem.code" 
                            label="Código" 
                            variant= outlined 
                            dense
                        ></v-text-field>
                        <v-text-field 
                            v-model="editedItem.description"
                            label="Descrição" 
                            variant= outlined 
                            dense
                        ></v-text-field>
                        <v-text-field
                            v-model="editedItem.category" 
                            label="Categoria" 
                            variant= outlined 
                            dense
                        ></v-text-field>
                        <v-text-field 
                            v-model="editedItem.brand"
                            label="Marca" 
                            variant= outlined 
                            dense
                        ></v-text-field>
                        <v-text-field
                            v-model="editedItem.price" 
                            label="Preço Unitário" 
                            variant= outlined 
                            dense
                        ></v-text-field>
                        <v-text-field 
                            v-model="editedItem.variation" 
                            label="Variação" 
                            variant="outlined" 
                            dense
                        ></v-text-field>

                        <v-row>
                            <v-col cols="12">
                                <v-btn color="#4ec21c" block :style="{color: 'white'}" @click="includeItems"><v-icon left>mdi-plus-circle-outline</v-icon>Incluir</v-btn>
                            </v-col>
                            <v-col cols="12">
                                <v-btn color="#cc0000" block :style="{color: 'white'}" @click="deleteSelectedItems"><v-icon left>mdi-delete</v-icon>Excluir</v-btn>
                            </v-col>
                        </v-row>

                        <v-row>
                            <v-col>
                                <p :style="{color: 'black', textAlign: 'center', fontWeight: 'bold'}">Quantidade de Peças: {{items.length}}</p>
                            </v-col>
                        </v-row>

                        <v-btn color="blue" block :style="{ color: 'white'}" @click="saveOrder">SALVAR</v-btn>
                     </v-col>
                </v-row>

                <v-row>
                    <v-col cols="12">
                        <div :style="{ background: '#4a4a4a', color: 'white', padding: '20px', textAlign: 'start'}">
                            <strong>TOTAL: {{ totalCompra.toFixed(2) }}</strong>
                        </div>
                    </v-col>
                </v-row>

                

            </v-container>
        </v-main>


    </v-app>
</template>

<script>
import { computed } from 'vue';
import axios from "axios"
import api from '@/services/api';

export default {
    data() {
        return {
            clientId: '',
            client: {
                name: '',
                cpf: '',
            },
            status:'',
            closingDate: '',
            statusOptions: ['Pendente', 'Aguardando encerramento', 'Fechado'],
            headers: [
                {title: 'Id do Produto', align: 'center', value:'id'},
                {title: 'Código', align: 'center', value:'code'},
                {title: 'Descrição', align: 'center', value:'description'},
                {title: 'Categoria', align: 'center', value:'category'},
                {title: 'Marca', align: 'end', value:'brand'},
                {title: 'Preço Unitário', align: 'center', value:'price'},
                {title: 'Subtotal', align: 'center', value:'subtotal'},
            ],
            editedIndex: -1,
            editedItem: {
                id: '',
                code: '',
                description: '',
                category: '',
                brand: '',
                price:'',
                variation: '',
                subtotal:'',
            },
            defaultItem: {
                id: '',
                code: '',
                description: '',
                category: '',
                brand: '',
                price:'',
                variation: '',
                subtotal:'',
            },
            items: [],
            selectedItems: [], // armazena os items selecionados pelo ID
            quantity: '',
            listItems: [],
            total: [],
            productId: '',
            observation: '', 
            
            successMessage: '',
            errorMessage: '',
        };
    },

    methods: {
        clearErrorMessage() {
            this.errorMessage = '';
        },
        includeItems() {
            if ( this.editedItem.id && 
                this.editedItem.code && 
                this.editedItem.description && 
                this.editedItem.category && 
                this.editedItem.brand && 
                this.editedItem.price && 
                this.editedItem.variation
            ) {
                const existingItem = this.items.find(item => item.id === this.editedItem.id);
                if (!existingItem) {
                    const newItem = {
                        id: this.editedItem.id,
                        code: this.editedItem.code,
                        description: this.editedItem.description,
                        category: this.editedItem.category,
                        brand: this.editedItem.brand,
                        price: this.editedItem.price,
                        variation: this.editedItem.variation,
                        subtotal: this.editedItem.price
                    };
                    this.items.push(newItem);
                } else {
                    this.errorMessage = 'Este produto já foi adicionado.';
                }
                this.resetFields();
            } else {
                this.errorMessage = 'Preencha todos os campos antes de incluir.';
            }
        },

        fetchProductDetails() {
            if (this.productId) {
                api
                    .get(`http://localhost:3002/api/Product/${this.productId}`)
                    .then((res) => {
                        console.log(res.data);
                        const product = res.data.result;
                        if (product) {
                            this.editedItem = {
                                id: this.productId,
                                code: product.code,
                                description: product.description,
                                category: product.category,
                                brand: product.brand,
                                price: product.price,
                            };
                        } else {
                            this.errorMessage = 'Produto não encontrado.';
                        }
                    })
                    .catch((error) => {
                        this.errorMessage = 'Erro ao buscar produto.';
                        console.error(error);
                    });
            } else {
                this.errorMessage = 'Por favor, insira um código de produto válido.';
            }
        
        },
        
        deleteSelectedItems() {
            if(this.selectedItems.length) {
                this.items = this.items.filter(item => !this.selectedItems.includes(item.id));
                this.selectedItems = [];
            }
        },

        resetFields() {
            this.id = '';
            this.quantity = '';
            this.editedItem = { ...this.defaultItem };
            this.editedItem.variation = '';
        },

        fetchClientDetails() {
            if (this.clientId) {
                api
                    .get(`http://localhost:3002/api/Customer/${this.clientId}`)
                    .then((res) => {
                        const client = res.data.result;
                        if (client) {
                            this.client = {
                                id: client.id,
                                name: `${client.firstName} ${client.lastName}`,
                                cpf: client.cpf,
                            };
                            this.errorMessage = '';
                        } else {
                            this.errorMessage = 'Cliente não encontrado.';
                        }
                    })
                    .catch((error) => {
                        this.errorMessage = 'Erro ao buscar cliente.';
                        console.error(error);
                    });
            } else {
                this.errorMessage = 'Por favor, insira um ID de cliente válido.';
            } 
        },

        saveOrder() {
            // Cria o payload no formato esperado pela API
            const orderPayload = {
                customerId: this.clientId, // ID do cliente
                observation: this.observation, // Inclui a objservação do pedido
                listCreatedItem: this.items.map(item => ({
                    productId: item.id, // ID do produto
                    variation: item.variation // Envia a varição do produto
                }))
            };

            // Faz o POST para a API
            api
            .post('http://localhost:3002/api/Order', orderPayload)
            .then((res) => {
                this.successMessage = 'Condicional salva com sucesso!';
                this.errorMessage = '';
                console.log('Condicional salva:', res.data);
            })
            .catch((error) => {
                this.errorMessage = 'Erro ao salvar a condicional.';
                this.successMessage = '';
                console.error(error);
            });

        },

    },
    computed: {
        totalCompra() {
            // Reduce porque é um array e não há a função sum em javascript
            return this.items.reduce((total, item) => {
                return total + item.price;
            }, 0);
        },
    },  
};
</script>


<style scoped>
h1 {
  font-family: 'Montserrat';
}

.custom-data-table {
  background-color: #585147; /* Cor de fundo da tabela */
  color: #ffffff; /* Cor do texto */
}


</style>