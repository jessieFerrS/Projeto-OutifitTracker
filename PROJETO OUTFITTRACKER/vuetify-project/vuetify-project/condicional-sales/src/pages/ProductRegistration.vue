<template>
    <v-app
      id="inspire"
      :style="{ background: '#C8C8C1'}">
      <NavigationDrawer/>
     
        <v-main>
          <v-container fluid fill-height class="pa-2-md-5">
            <v-icon size="36" class="d-none d-md-inline-block">mdi mdi-package-variant-plus</v-icon>
            <v-icon size="24" class="d-inline-block d-md-none">mdi mdi-package-variant-plus</v-icon>
            <h1>Cadastrar Produto</h1>
            <br />

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
                <!-- INPUTS FORMS -->
                <v-row class="product">
                  <v-col cols="6">
                    <v-text-field
                      v-model="code"
                      :counter="20"
                      :type="String"
                      :rules="[v => !!v || 'Campo obrigatório']"
                      label="Código do Produto"
                      variant="outlined"
                      required
                    ></v-text-field>
                    <v-text-field
                      v-model="description"
                      :counter="100"
                      type="String"
                      :rules="[v => !!v || 'Campo obrigatório']"
                      label="Descrição do Produto"
                      variant="outlined"
                      required
                    ></v-text-field>
                    <v-text-field
                      v-model="price"
                      :counter="10"
                      :type="Double"
                      :rules="[v => !!v || 'Campo obrigatório']"
                      label="Preço do Produto"
                      variant="outlined"
                      prefix="R$"
                      required
                    ></v-text-field>
                    <v-text-field
                      v-model="brand"
                      :counter="50"
                      type="String"
                      :rules="[v => !!v || 'Campo obrigatório']"
                      label="Marca do Produto"
                      variant="outlined"
                      required
                    ></v-text-field>
                    <v-text-field
                      v-model="category"
                      :counter="100"
                      type="String"
                      :rules="[v => !!v || 'Campo obrigatório']"
                      label="Categoria do Produto"
                      variant="outlined"
                      required
                    ></v-text-field>
                  </v-col>
                  <v-col cols="6">
                      <v-img src="../assets/reposicao_produtos_loja.jpg" cover></v-img>
                  </v-col>
                </v-row>
                
                <!-- BOTÕES -->
                <v-btn
                  class="me-4"
                  type="submit"
                  color="success"
                >Salvar
                </v-btn>
                
                <v-btn
                  class="me-4"
                  color="error"
                  @click="reset"
                >Limpar
                </v-btn>
  
                <v-btn
                  class="me-4"
                  color="warning"
                  @click="resetValidation"
                >Cancelar
                </v-btn>
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

  export default {
    data: () => ({
      code: '',
      description: '',
      price: '',
      brand: '',
      category: '',

      successMessage: '',
      errorMessage: ''
    }),
    methods: {
      async submit() {
        const productData = {
          code: this.code,
          description: this.description,
          price: this.price,
          brand: this.brand,
          category: this.category,
        };

        this.postProduct(productData);

      },

      postProduct(productData) {
        console.log(productData);
        api
          .post("http://localhost:3002/api/Product", productData, {
            headers: {
              'Content-Type': 'application/json'
            }
          })
          .then(() => {
            console.log('Produto Cadastrado com Sucesso!');
            this.successMessage = 'Produto Cadastrado com Sucesso!';
          })
          .catch((error) => {
            console.log(error);
            this.errorMessage = 'Erro ao Cadastrar Produto';
          });
      },

      reset() {
        this.$refs.form.reset();
        this.successMessage = '';
        this.errorMessage = '';
      },
      resetValidation() {
        this.$refs.form.resetValidation();
      },
    },
  };
  </script>
  
  <style scoped>
  h1 {
    font-family: 'Montserrat';
  }
  </style>