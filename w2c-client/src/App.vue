<template>
  <div id="app">
    <header>
      <nav>
        <p><strong>wContacts</strong></p>
        <img src=""
             alt="wLogo"/>
      </nav>
    </header>


    <main>

      <div class="sidebar">
        <h3 v-if="editClientID < 0">NOVO CONTATO</h3>
        <h3 v-else>EDITAR CONTATO</h3>
        <CadastroForm :clienteID="editClientID" :concessionarias="concessionarias" @update="updateData" @done="reloadData" @cancelled="cancelEdit"> </CadastroForm>
      </div>

      <div class="contacts-area">

        <span v-for="cliente in clientes" :key="cliente.id">
             <ContatoCard :cliente="cliente" :concessionaria="getConcessionaria(cliente.concessionariaID)" @editRequest="editClient" ></ContatoCard>
        </span>
      </div>
    </main>
    <footer>
    </footer>
  </div>
</template>

<script>

import CadastroForm from "./components/CadastroForm";
import ContatoCard from "./components/ContatoCard";
import { ClienteResource, ConcessionariaResource } from './utils/api.js';

export default {
  name: 'app',
  components: {
    ContatoCard,
    CadastroForm,
  },
  computed: {

  },
  created() {

    this.clientesApi = new ClienteResource(this.$resource);
    this.concessApi = new ConcessionariaResource(this.$resource);

    this.clientesApi.list()
            .then(x => this.clientes = x);

    this.concessApi.list()
            .then(x => this.concessionarias = x);

  },
  methods: {
    editClient(id) {
      this.editClientID = id;
    },
    cancelEdit() {
      this.editClientID = -1;
    },
    updateData() {
      this.concessApi.list()
              .then(x => this.concessionarias = x);
      this.$forceUpdate();
    },
    reloadData() {
      this.clientesApi.list()
              .then(x => this.clientes = x);
      this.$forceUpdate();
      this.editClientID = -1;
    },
    getConcessionaria(requestedId) {
      let clientConces = this.concessionarias.find(c => (c.id === requestedId));
      return clientConces;
    },
  },
  data() {
    return {
      concessionariaID: -1,
      concessionarias: [],

      clientes: [],
      editClientID: -1,
      admin: false,
    }
  }
}



</script>

<style>
#app {
  font-family: 'Segoe UI', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  /*margin-top: 10px;*/
}

body {
  margin: 0;
  padding: 0;
}

  nav {
    display: flex;
    justify-content: space-between;
    align-items:center;
    background-color: white;
    border-bottom: 3px solid #4287f5
  }

  nav img {
    width: 120px;
    height: 36px;
    margin: 0 15px;
    background-color: #4287f5;
  }

  nav p {
    font-size: 1.5em;
    margin: 10px 20px;
  }

  main {
    display: flex;
    flex-direction: row;
    align-items: flex-start;
  }
  .sidebar {
    width: 300px;
    border: 1px solid darkgray;
    margin-top: 30px;
    margin-left: 30px;
    box-shadow: 0 0 14px 0 rgba(0, 0, 0, 0.2);
  }

  .sidebar h3 {
    margin: 15px auto;
  }

  .contacts-area {
    padding-top: 30px;
    display: grid;
    grid-template-columns: repeat(4, 1fr);
    gap: 30px;
    margin: 0 auto;
  }

  html, body, #app {

  }

  ul {
    list-style: none;
    margin: 0;
    padding: 0;
  }

</style>
