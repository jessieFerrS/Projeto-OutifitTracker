<template>
    <v-app
      id="inspire"
      :style="{ background: '#C8C8C1' }">
      <NavigationDrawer/>
     
      <v-main>
        <v-container fluid fill-height class="pa-2-md-5">
          <v-icon size="36" class="d-none d-md-inline-block">mdi mdi-account-group-outline</v-icon>
          <v-icon size="24" class="d-inline-block d-md-none">mdi mdi-account-group-outline</v-icon> 
          <h1 >Lista de Clientes</h1>
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
                :items= "clients"
                :search="search"
                :sort-by="[{ key: 'firstName', order: 'asc' }]"
              >
              <template v-slot:item.birthDate="{ item }">
                {{ formatDate(item.birthDate) }}
              </template>

              <template v-slot:item.creationDate="{ item }">
                {{ formatDate(item.creationDate) }}
              </template>

              <template v-slot:item.stateAbbreviation="{ item }">
                {{ convertStateToAbbreviation(item.stateAbbreviation) }}
              </template>



                <template v-slot:top>
                  <v-toolbar
                    flat
                    color="#545454"
                  >
                    <v-toolbar-title>Buscar Cliente por CPF</v-toolbar-title>
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
                          label="00000000000"
                          v-mask="'###########'" 
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
                              <!-- Dados Editáveis -->
                              <v-col
                                cols="12"
                                md="4"
                                sm="6"
                              >
                                <v-text-field
                                  v-model="editedItem.firstName"
                                  label="Nome"
                                ></v-text-field>
                              </v-col>
                              <v-col
                                cols="12"
                                md="4"
                                sm="6"
                              >
                                <v-text-field
                                  v-model="editedItem.lastName"
                                  label="Sobrenome"
                                ></v-text-field>
                              </v-col>
        
                              <v-col
                                cols="12"
                                md="4"
                                sm="6"
                              >
                                <v-text-field
                                  v-model="editedItem.email"
                                  label="Email"
                                ></v-text-field>
                              </v-col>

                              <v-col
                                cols="12"
                                md="4"
                                sm="6"
                              >
                                <v-text-field
                                  v-model="editedItem.mobilePhoneNumber"
                                  label="Telefone"
                                ></v-text-field>
                              </v-col>
                              <v-col
                                cols="12"
                                md="4"
                                sm="6"
                              >
                                <v-text-field
                                  v-model="editedItem.cityName"
                                  label="Cidade"
                                ></v-text-field>
                              </v-col>
                              <v-col
                                cols="12"
                                md="4"
                                sm="6"
                              >
                                <v-text-field
                                  v-model="editedItem.street"
                                  label="Rua/Avenida"
                                ></v-text-field>
                              </v-col>
                              <v-col
                                cols="12"
                                md="4"
                                sm="6"
                              >
                                <v-text-field
                                  v-model="editedItem.number"
                                  label="Número"
                                ></v-text-field>
                              </v-col>
                              <v-col
                                cols="12"
                                md="4"
                                sm="6"
                              >
                                <v-text-field
                                  v-model="editedItem.postalCode"
                                  label="CEP"
                                ></v-text-field>
                              </v-col>
                              <v-col
                                cols="12"
                                md="4"
                                sm="6"
                              >
                                <v-text-field
                                  v-model="editedItem.complement"
                                  label="Complemento"
                                ></v-text-field>
                              </v-col>
                              <v-col
                                cols="12"
                                md="4"
                                sm="6"
                              >
                                <v-text-field
                                  v-model="editedItem.neighborhood"
                                  label="Bairro"
                                ></v-text-field>
                              </v-col>
                              <v-col
                                cols="12"
                                md="4"
                                sm="6"
                              >
                                <v-text-field
                                  v-model="editedItem.stateAbbreviation"
                                  label="Estado"
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
                        ><br>Você tem certeza que deseja excluir esse cliente?</v-card-title>
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
                  <v-icon class="me-2" size="small" @click="editItem(item)">mdi-pencil</v-icon>
                  <v-icon size="small" @click="deleteItem(item)">mdi-delete</v-icon>
                </template>

                <template v-slot:no-data>
                  <v-btn color="black" @click="initialize">Reset</v-btn>
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
        {title: 'ID', align: 'start', value: 'id'},
        {title: 'Nome', align: 'start', sortable: false, value: 'firstName'},
        { title: 'Sobrenome', value: 'lastName' , sortable: false},
        { title: 'Email', value: 'email' },
        { title: 'Data de Nascimento', value: 'birthDate' },
        { title: 'CPF', value: 'cpf' },
        { title: 'RG', value: 'rg' },
        { title: 'Telefone', value: 'mobilePhoneNumber' },
        { title: 'Rua/Avenida', value: 'street' },
        { title: 'Número', value: 'number' },
        { title: 'Complemento', value: 'complement' },
        { title: 'Bairro', value: 'neighborhood' },
        { title: 'CEP', value: 'postalCode' },
        { title: 'Cidade', value: 'cityName' },
        { title: 'Estado', value: 'stateAbbreviation' },
        { title: 'Data Cadastro', value: 'creationDate' },
        { title: 'Ações', value: 'actions', sortable: false },
      ],
      clients: [],
      editedIndex: -1,
      editedItem: {
        id: '',
        firstName: '',
        lastName: '',
        email: '',
        birthDate: '',
        cpf: '',
        rg: '',
        mobilePhoneNumber: '',
        street: '',
        number: '',
        complement: '',
        neighborhood: '',
        postalCode: '',
        cityName: '',
        stateAbbreviation: '',
        creationDate: '',
      },
      defaultItem: {
        id: '',
        firstName: '',
        lastName: '',
        email: '',
        birthDate: '',
        cpf: '',
        rg: '',
        mobilePhoneNumber: '',
        street: '',
        adressNumber: '',
        complement: '',
        neighborhood: '',
        postalCode: '',
        cityName: '',
        stateAbbreviation: '',
        creationDate: '',
      },

      successMessage: '',
      errorMessage: '',

      stateMapping: {
        0: "AC",
        1: "AL",
        2: "AP",
        3: "AM",
        4: "BA",
        5: "CE",
        6: "DF",
        7: "ES",
        8: "GO",
        9: "MA",
        10: "MT",
        11: "MS",
        12: "MG",
        13: "PA",
        14: "PB",
        15: "PR",
        16: "PE",
        17: "PI",
        18: "RJ",
        19: "RN",
        20: "RS",
        21: "RO",
        22: "RR",
        23: "SC",
        24: "SP",
        25: "SE",
        26: "TO",
        27: "PA"
      }
    }),
 
    computed: {
      formTitle () {
        return this.editedIndex === -1 ? 'Editar item': 'Editar item'
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
      this.getCustomer();
    },
 
    methods: {
      getCustomer() {
        api
          .get('http://localhost:3002/api/Customer')
          .then((res) => {
            console.log(res.data.result);
            this.clients = res.data.result || [];
            console.log(this.clients);
          }) 
          .catch((error) => {
            console.log(error);
          });
      },

      convertStateToAbbreviation(stateNumber) {
        return this.stateMapping[stateNumber] || 'N/A'; // Retorna 'N/A' se o estado não for encontrado
      },

      initialize() {
        this.getCustomer();
      },
 
      editItem (item) {
        this.editedIndex = this.clients.indexOf(item)
        this.editedItem = Object.assign({}, item)
        this.dialog = true
      },
 
      deleteItem (item) {
        this.editedIndex = this.clients.indexOf(item)
        this.editedItem = Object.assign({}, item)
        this.dialogDelete = true
      },
 
      deleteItemConfirm () {
        if (this.editedItem.id) {
          api
            .delete(`http://localhost:3002/api/Customer/${this.editedItem.id}`)
            .then(() => {
              this.clients.splice(this.editedIndex, 1); // Remove o cliente da lista local
              this.successMessage = 'Cliente excluído com Sucesso!'; // Mensagem de sucesso
              this.closeDelete();
            })
            .catch((error) => {
              this.errorMessage = 'Erro ao Excluir Cliente!';
              console.error(error);
            });
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
        if (this.editedItem.id) {
          // Se houver um ID, o update será feito
          api
          .put(`http://localhost:3002/api/Customer/${this.editedItem.id}`, this.editedItem)
          .then(() => {
            Object.assign(this.clients[this.editedIndex], this.editedItem);
            this.successMessage = 'Cliente Editado com Sucesso!'; // Mensagem de sucesso
            this.close();
          })
          .catch((error) => {
            this.errorMessage = 'Erro ao Editar Cliente. Dado em Formato Inválido!';
            console.error(error);
          });
      } else {
        api
          .post('http://localhost:3002/api/Customer', this.editedItem)
          .then((res) => {
            this.clients.push(res.data);
            this.close();
          })
          .catch((error) => {
            this.errorMessage = 'Erro ao Cadastrar Cliente. Dado em Formato Inválido!';
            console.error(error);
          });
        }
      },

      // FORMATA DATAS
      formatDate(date) {
        if(!date) return '';
        const [year, month, day] = date.split('T')[0].split('-');
        return `${day}-${month}-${year}`;
      },

    },
}
</script>
 
<style scoped>
h1 {
  font-family: 'Montserrat';
}

 
</style>