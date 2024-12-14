<template>
    <v-app
      id="inspire"
      :style="{ background: '#C8C8C1'}">
      <NavigationDrawer/>
      <v-main>
        <v-container fluid fill-height class="pa-2-md-5">
          <v-icon size="36" class="d-none d-md-inline-block">mdi mdi-account-plus</v-icon>
          <v-icon size="24" class="d-inline-block d-md-none">mdi mdi-account-plus</v-icon>
          <h1>Cadastrar Cliente</h1>
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
   
            <v-sheet 
                class="mx-auto" 
                width="900"
                elevation="10"
                rounded
                :style="{ backgroundColor: '#8e8d8d', padding: '50px', color: '#ffffff' }"
                >
              <v-form ref="form" @submit.prevent="submit">
                <!-- DADOS CLIENTE -->
                <v-row class="name">
                  <v-col cols="6">
                    <v-text-field
                      v-model="firstName"
                      :counter="50"
                      type="String"
                      variant="outlined"
                      :rules="[v => !!v || 'Campo obrigatório']"
                      label="Nome"
                      required
                    ></v-text-field>
                  </v-col>
                  <v-col cols="6">
                    <v-text-field
                      v-model="lastName"
                      :counter="50"
                      type="String"
                      variant="outlined"
                      :rules="[v => !!v || 'Campo obrigatório']"
                      label="Sobrenome"
                      required
                    ></v-text-field>
                  </v-col>
 
                  <!-- DATA NASCIMENTO -->
                  <v-col cols="6">
                    <v-text-field
                      label="Data de Nascimento"
                      v-model="birthDate"
                      variant="outlined"
                      :rules="[v => !!v || 'Campo obrigatório']"
                      type="date"
                    ></v-text-field>
                  </v-col>
                 
                  <!-- CPF -->
                  <v-col cols="6">
                    <v-text-field
                      v-model="cpf"
                      :counter="11"
                      type="String"
                      :rules="[v => !!v || 'Campo obrigatório']"
                      label="CPF"
                      variant="outlined"
                      required
                    ></v-text-field>
                  </v-col>
 
                  <!-- RG -->
                  <v-col cols="6">
                    <v-text-field
                      v-model="rg"
                      :counter="9"
                      type="String"
                      label="RG"
                      variant="outlined"
                    ></v-text-field>
                  </v-col>
 
                  <!-- EMAIL -->
                  <v-col cols="6">
                    <v-text-field
                      v-model="email"
                      :counter="256"
                      type="String"
                      :rules="[v => !!v || 'Campo obrigatório']"
                      label="Email"
                      variant="outlined"
                      hint="usuario@example.com"
                      required
                    ></v-text-field>
                  </v-col>
 
                </v-row>
 
                <!-- ENDEREÇO COMPLETO -->
                <v-row class="adress">
 
                  <v-col cols="6">
                    <v-text-field
                      v-model="street"
                      :counter="50"
                      type="String"
                      :rules="[v => !!v || 'Campo obrigatório']"
                      label="Rua/Avenida"
                      variant="outlined"
                      required
                    ></v-text-field>
                  </v-col>
                  <v-col cols="6">
                    <v-text-field
                      v-model="complement"
                      :counter="50"
                      type="String"
                      label="Complemento"
                      variant="outlined"
                      required
                    ></v-text-field>
                  </v-col>
                  <v-col cols="6">
                    <v-text-field
                      v-model="number"
                      :counter="10"
                      type="Number"
                      :rules="[v => !!v || 'Campo obrigatório']"
                      label="Número"
                      variant="outlined"
                    ></v-text-field>
                  </v-col>
                  <v-col cols="6">
                    <v-text-field
                      v-model="neighborhood"
                      :counter="50"
                      type="String"
                      :rules="[v => !!v || 'Campo obrigatório']"
                      label="Bairro"
                      variant="outlined"
                      required
                    ></v-text-field>
                  </v-col>
                  <v-col cols="6">
                    <v-text-field
                      v-model="cityName"
                      :counter="50"
                      type="String"
                      :rules="[v => !!v || 'Campo obrigatório']"
                      label="Cidade"
                      variant="outlined"
                      required
                    ></v-text-field>
                  </v-col>
                  <v-col cols="6">
                    <v-autocomplete
                      ref="state"
                      v-model="stateAbbreviation"
                      :items="states"
                      :rules="[v => !!v || 'Campo obrigatório']"
                      label="Estado"
                      variant="outlined"
                      placeholder="Selecione"
                      required
                    ></v-autocomplete>
                  </v-col>
                  <v-col cols="6">
                    <v-text-field
                      v-model="postalCode"
                      :counter="8"
                      type="String"
                      :rules="[v => !!v || 'Campo obrigatório']"
                      label="CEP"
                      variant="outlined"
                      @blur="fetchAddress"
                      required
                    ></v-text-field>
                  </v-col>
 
                  <!-- TELEFONE -->
                  <v-col cols="6">
                    <v-text-field
                    v-model="mobilePhoneNumber"
                    :counter="13"
                    type="String"
                    :rules="[v => !!v || 'Campo obrigatório']"
                    label="Telefone Celular"
                    variant="outlined"
                    required
                    ></v-text-field>
                  </v-col>
 
                </v-row>
 
                <!-- BOTÕES -->
              <div class="d-flex">
                <v-btn
                  class="me-4"
                  type="submit"
                  color="success"
                >Salvar</v-btn>
               
                <v-btn
                  class="me-4"
                  color="error"
                  @click="reset">
                    Limpar
                </v-btn>
 
                <v-btn
                  class="me-4"
                  color="warning"
                  @click="resetValidation"
                  >Cancelar
                </v-btn>
              </div>
              </v-form>
            </v-sheet>
        </v-container>
      </v-main>  
    </v-app>
  </template>
 
  <script setup>
  import NavigationDrawer from "@/components/NavigationDrawer.vue";
  </script>
 
  <script>
  import api from "../services/api";
  import axios from "axios";

  export default {
    data: () => ({
      states: ['AC', 'AL', 'AP', 'AM', 'BA', 'CE', 'DF', 'ES', 'GO', 'MA', 'MG', 'MS', 'MT', 'PA', 'PB', 'PE', 'PI', 'PR', 'RJ', 'RO', 'RN', 'RR', 'RS', 'SC', 'SE', 'SP', 'TO' ],
      stateAbbreviation: '',
      firstName: '',
      lastName: '',
      street: '',
      number: '',
      neighborhood: '',
      postalCode: '',
      complement: '',
      cityName: '',
      email: '',
      mobilePhoneNumber: '',
      cpf: '',
      rg: '',
      birthDate: '',

      successMessage: '',
      errorMessage: ''
    }),

    methods: {
      async fetchAddress() {
        if (this.postalCode.length === 8) {
          try {
            const response = await axios.get(`https://brasilapi.com.br/api/cep/v2/${this.postalCode}`);
            const data = response.data;

            // Preenche os campos de endereço com os dados retornados
            this.street = data.street;
            this.neighborhood = data.neighborhood;
            this.cityName = data.city;
            this.stateAbbreviation = data.state;
          } catch (error) {
            this.errorMessage = 'Erro ao buscar endereço. Verifique o CEP.';
            console.error(error);
          }
        }
      },
      async submit() {
        const clientData = {
          firstName: this.firstName,
          lastName: this.lastName,
          birthDate: this.birthDate,
          cpf: this.cpf,
          rg: this.rg || '',
          email: this.email,
          street: this.street,
          complement: this.complement || '',
          number: parseInt(this.number) || 0,
          neighborhood: this.neighborhood,
          cityName: this.cityName,
          stateAbbreviation: this.stateAbbreviation,
          postalCode: this.postalCode,
          mobilePhoneNumber: this.mobilePhoneNumber,
        };
        
        this.postCustomer(clientData);

      },

      postCustomer(clientData) {
        console.log(clientData);
        api
          .post("http://localhost:3002/api/Customer", clientData, {
            headers: {
              'Content-Type': 'application/json'
            }
          })
          .then(() => {
            console.log('Usuário Cadastrado com Sucesso!');
            this.successMessage = 'Usuário Cadastrado com Sucesso!';
          })
          .catch((error) => {
            console.log(error);
            this.errorMessage = 'Erro ao cadastrar usuário. Preenchimento Incorreto dos Campos.';
          });
      },

      reset () {
      this.$refs.form.reset();
      this.successMessage = '';
      this.errorMessage = '';
      },
      resetValidation () {
        this.$refs.form.resetValidation()
      },
    },
  };
  </script>
 
<style scoped>
h1 {
  font-family: 'Montserrat';
}

@media (min-width: 960px) {
  .v-navigation-drawer {
    display: flex !important;
  }
  .v-app-bar {
    display: none !important;
  }
}

@media (max-width: 959px) {
  .v-navigation-drawer {
    display: none !important;
  }
  .v-app-bar {
    display: flex !important;
  }
}

</style>