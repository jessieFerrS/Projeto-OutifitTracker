<template>
  <v-navigation-drawer 
      v-model="drawer" 
      color="#624229"
      app
      width="300"
      :permanent="!isMobile"
      mobile-breakpoint="960" 
  >
      <div>
          <v-img 
          src="../assets/OutFitTrackLogo.png"
          alt="Logo Outif Tracker"
          width="150"
          height="150"
          class="ml-10"
          ></v-img>
      </div>
      <v-card class="rounded-e-xl me" width="60" height="500" color="#8e8d8d">
      </v-card>
      <v-list class="top">
          <v-list-item
          v-for="(item, i) in links"
          :key="i"
          :value="item"
          :to="item.route"
          class="mb-4"
          color="#000000"
          variant="plain"
          @click="navigateToPage(item.route)">
              <template v-slot:append="{isActive}">
                  <div v-if="isActive" class="a"></div>
              </template>

              <template v-slot:prepend>
                  <v-icon :icon="item.icon"></v-icon>
              </template>
              <v-list-item-title v-text="item.text"></v-list-item-title>
          </v-list-item>
      </v-list>
      <div style="position: absolute; bottom: 20px; margin-left: auto; margin-right: auto; left: 0; right: 0; text-align: center;"></div>
  </v-navigation-drawer>
  

  <v-app-bar app color='#624229' elevation="0">
      <v-app-bar-nav-icon v-if="isMobile" @click="drawer = !drawer"></v-app-bar-nav-icon>
  </v-app-bar>

</template>

<script setup>
import { ref, watch, onMounted, onBeforeUnmount } from "vue";
import { useRouter } from "vue-router";

// Router
const router = useRouter();

// Controla a abertura e fechamento do drawer
const drawer = ref(true);  // Agora começa como true por padrão para desktops

// Links de navegação
const links = [
{ text: "Cadastrar Cliente", icon: "mdi mdi-account-plus-outline", route: '/CustomerRegistration' },
{ text: "Clientes", icon: "mdi mdi-account-edit", route: "/ClientsPage" },
{ text: "Cadastrar Produto", icon: "mdi mdi-archive-plus-outline", route: '/ProductRegistration' },
{ text: "Produtos", icon: "mdi mdi-hanger", route: "/ProductsPage" },
{ text: "Criar Condicional", icon: "mdi mdi-file-document-plus-outline", route: "/ConditionalPage" },
{ text: "Histórico Condicionais", icon: "mdi mdi-file-compare", route: "/ConditionalsHistory" },
{ text: "Sair", icon: "mdi mdi-location-exit", route: '/' },
];

// Função para redirecionar para a página correspondente
const navigateToPage = (item) => {
if (item.text === "Sair") {
  router.push({ path: item.route });
} else {
  router.push({ path: item.route });
}
};

// Responsividade
const isMobile = ref(false);

// Verifica o tamanho da tela e ajusta o comportamento do drawer
const updateIsMobile = () => {
isMobile.value = window.innerWidth < 960;
drawer.value = !isMobile.value;  // Se for desktop, o drawer começa como aberto, se for mobile, começa fechado
};

onMounted(() => {
window.addEventListener('resize', updateIsMobile);
updateIsMobile();  // Para verificar no carregamento inicial
});

onBeforeUnmount(() => {
window.removeEventListener('resize', updateIsMobile);
});

// Observa mudanças no tamanho da tela para atualizar o estado do drawer
watch(isMobile, (newValue) => {
drawer.value = !newValue;  // Quando em desktop, drawer sempre aberto, quando em mobile, ele inicia fechado
});

</script>

<style scoped>
.v-list-item.v-list-item--active.v-list-item--link.border.v-theme--light.text-red.v-list-item--density-default.v-list-item--one-line.v-list-item--variant-text {
color: white !important;
}
.top {
margin-top: 60px;
}
.me {
position: absolute;
margin-top: 28px;
}
.v-locale--is-ltr .rounded-e-xl {
border-top-right-radius: 44px !important;
border-bottom-right-radius: 44px !important;
}
.a {
top: 32%;
left: 18%;
position: absolute;
height: 15px;
width: 15px;
background: linear-gradient(-90deg, #545454, #545454 50%, black 50%);
border-radius: 50%;
}
</style>
