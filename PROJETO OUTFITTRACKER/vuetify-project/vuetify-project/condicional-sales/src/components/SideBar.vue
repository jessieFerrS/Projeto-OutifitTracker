<template>
  <!-- MENU HAMBURGUER -->
  <v-app-bar :elevation="0">
    <template v-slot:prepend>
      <v-app-bar-nav-icon @click.stop="menuOpen = !menuOpen"></v-app-bar-nav-icon>
      <!-- Dropdown Menu -->
      <v-menu v-model="menuOpen" offset-y>
        <v-list>
          <!-- Utilize router-link para navegação -->
          <router-link to="/customerRegistration" tag="v-list-item">
            <v-list-item-icon>
              <v-icon style="margin: 1rem;">mdi-login</v-icon>
            </v-list-item-icon>
            <v-list-item-title style="margin: 1rem;">ENTRAR</v-list-item-title>
          </router-link>
        </v-list>
      </v-menu>
      <!-- /Dropdown Menu -->
    </template>
  </v-app-bar>
</template>

<script>
export default {
  data() {
    return {
      menuOpen: false
    };
  }
};
</script>
