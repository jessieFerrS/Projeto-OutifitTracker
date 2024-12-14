<template>
  <v-app>
    <SideBar/>
    <v-img
      src="../assets/tela-inicial.jpg"
      height="100vh"
      cover
      
    >
      <!-- Overlay com fundo semitransparente e centralização -->

      <v-main class="d-flex align-center justify-center" style="min-height: 600px;">
        <v-container style="background-color: rgba(212, 199, 180, 0.5); height: 60vh; width: 60%; font-size: 1.5em;">
          <v-row justify="center" style="flex: 1;">
            <v-col class="text-center">
              <h1 style="color: #FFFFFF; margin-top: 20vh;">Outfit Tracker</h1>
              <h3 class="subheading font-italic" style="color: #585147; height: 80vh;">Your sales tracker</h3>
            </v-col>
          </v-row>
        </v-container>
      </v-main>
      <!-- Rodapé com Copyright -->
      <v-col class="text-center" style="color: white; border-top: 1px solid #D4C7B4; padding-top: 20px;">
        <small style="font-size: 1.1em;">© Copyright 2024</small>
      </v-col>

    </v-img>
  </v-app>
</template>

<style>
  

</style>

