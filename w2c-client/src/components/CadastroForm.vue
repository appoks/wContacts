<template>
    <form class="cadForm" @submit.prevent>

        <div class="inputGroup">
            <label for="empresa">Empresa</label>
            <input id="empresa" name="empresa" v-model="cliente.nomeEmpresa">
        </div>

        <div class="inputGroup">
            <label for="ramo">Ramo da Atividade</label>
            <input id="ramo" name="ramo" v-model="cliente.ramoAtividade">
        </div>

        <div class="side-by-side">
            <div class="inputGroup">
                <label for="cidade">Cidade</label>
                <input id="cidade" name="cidade" v-model="cliente.cidade">
            </div>

            <div class="inputGroup">
                <label for="estado">Estado</label>
                <input id="estado" name="estado" v-model="cliente.estado">
            </div>
        </div>

        <span class="separator"><p>-- CONTATO TÉCNICO --</p></span>

        <span class="addInfo"> </span>

        <div class="inputGroup">
            <label for="resp-cliente">Responsável</label>
            <input id="resp-cliente" name="resp-cliente" v-model="cliente.nomeResponsavel">
        </div>

        <div class="inputGroup">
            <label for="tel-cliente">Telefone</label>
            <input id="tel-cliente" name="tel-cliente" type="tel" v-model="cliente.telefone">
        </div>

        <div class="inputGroup">
            <label for="email-cliente">Email</label>
            <input id="email-cliente" name="email-cliente" type="email" v-model="cliente.email">
        </div>

        <span class="separator"><p>-- CONCESSIONÁRIA --</p></span>

        <div class="dropdown">
            <select v-model="concessionariaID">
                <option :value="-1" disabled>Selecione a Concessionária...</option>
                <option v-for="concessionaria in concessionarias" :key="concessionaria.id" :value="concessionaria.id">{{concessionaria.nome}} ({{concessionaria.contatoTecnico}})</option>
                <option :value="0">Adicionar Novo Contato...</option>
            </select>
        </div>

        <section class="concessionaria-detalhes" v-show="concessionariaID === 0 || editConcessionaria === true">

            <div class="inputGroup">
                <label for="concessionaria">Concessionária</label>
                <input id="concessionaria" name="concessionaria" v-model="concessionaria.nome">
            </div>

            <div class="inputGroup">
                <label for="resp-concess">Responsável</label>
                <input id="resp-concess" name="resp-concess" v-model="concessionaria.contatoTecnico">
            </div>

            <div class="inputGroup">
                <label for="tel-concess">Telefone</label>
                <input id="tel-concess" name="tel-concess" type="tel" v-model="concessionaria.telefone">
            </div>

            <div class="inputGroup">
                <label for="email-concess">Email</label>
                <input id="email-concess" name="email-concess" type="email" v-model="concessionaria.email">
            </div>

        </section>

        <div class="smallerButtonGroup">
            <button v-show="(concessionariaID > 0) && this.editConcessionaria === false" class="button-default" @click="detalharConcessionaria">EDITAR...</button>
            <button v-show="concessionariaID > 0 && this.editConcessionaria === true" class="button-danger" @click="deletarConcessionaria">REMOVER</button>
            <button v-show="concessionariaID === 0 || this.editConcessionaria === true" class="button-default" @click="cancelarEdicConcessionaria">CANCELAR</button>
            <button v-show="concessionariaID === 0 || this.editConcessionaria === true" class="button-default" @click="salvarConcessionaria">SALVAR</button>
        </div>

        <span class="separator"><p>-- CONFIRMAR OU CANCELAR --</p></span>

        <section class="buttonGroup">
            <button v-show="(clienteID < 0)" class="button-default" @click="criarCliente">ADICIONAR</button>
            <button v-show="(clienteID < 0)" class="button-default" @click="resetarForm">REINICIAR</button>
            <button v-show="!(clienteID < 0)" class="button-default" @click="alterarCliente">ALTERAR</button>
            <button v-show="!(clienteID < 0)" class="button-danger" @click="deletarCliente">REMOVER</button>
            <button v-show="!(clienteID < 0)" class="button-default" @click="cancelarEdicao">CANCELAR</button>

        </section>

    </form>
</template>

<script>
    import { ClienteResource, ConcessionariaResource } from '../utils/api.js';
    import Concessionaria from "../models/Concessionaria";
    import Cliente from "../models/Cliente";

    export default {
        name: "CadastroForm",
        props: {
            clienteID: {
                type: Number,
                required: true
            },
            concessionarias: {
                type: Array,
                required: true,
            }
        },
        data() {
            return {
                concessionariaID: -1,
                cliente: new Cliente(),
                concessionaria: new Concessionaria(),
                editConcessionaria: false,
            }
        },
        watch: {
            clienteID: function (value) {
                if (value < 0) {
                    return;
                }

                this.clientesApi.get(value)
                    .then((cl) => {
                        this.cliente = cl;
                        this.concessionariaID = this.cliente.concessionariaID;
                        this.concessApi.get(this.cliente.concessionariaID)
                            .then(cn => this.concessionaria = cn);
                    });
            },
            concessionariaID: function (value) {
                if (value === 0) {
                    this.concessionaria = new Concessionaria()
                }
                if (value < 1) {
                    return;
                }
                this.cliente.concessionariaID = value;
                this.concessApi.get(value)
                    .then(cn => this.concessionaria = cn);
            }

        },
        methods: {
            resetarForm() {
                this.cliente = new Cliente();
                this.concessionaria = new Concessionaria();
                this.concessionariaID = -1;
            },

            cancelarEdicao() {
                this.cancelarEdicConcessionaria();
                this.resetarForm();
                this.$emit('cancelled');
            },

            criarCliente() {
                this.clientesApi.add(this.cliente)
                    .then(cl => {
                        this.cliente = cl;
                        this.$emit('done');
                        this.resetarForm();
                    });
            },

            alterarCliente() {
                this.clientesApi.edit(this.cliente)
                    .then( () => {
                        this.$emit('done');
                        this.resetarForm();
                    });
            },
            deletarCliente() {
                this.clientesApi.remove(this.cliente.id)
                    .then( () => {
                        this.$emit('done');
                        this.resetarForm();
                    });
            },

            salvarConcessionaria: function () {

                if (this.concessionariaID > 0) {
                    this.concessApi.edit(this.concessionaria)
                        .then( cn => {
                            this.concessionaria = cn;
                            this.concessionariaID = this.concessionaria.id;
                            this.$emit('update');
                            this.cancelarEdicConcessionaria();
                        });
                } else if (this.concessionariaID === 0) {
                    this.concessApi.add(this.concessionaria)
                        .then( (cn ) => {
                            this.concessionaria = cn;
                            this.concessionariaID = this.concessionaria.id;
                            this.$emit('update');
                            this.cancelarEdicConcessionaria();
                        });
                }
            },

            deletarConcessionaria() {
                this.concessApi.remove(this.concessionariaID)
                    .then( () => {
                        this.$emit('update');
                        this.concessionariaID = -1;
                        this.cancelarEdicConcessionaria();
                    });

            },

            detalharConcessionaria() {
                this.editConcessionaria = true;
            },

            cancelarEdicConcessionaria() {
                this.editConcessionaria = false;
                if (this.concessionariaID === 0) {
                    this.concessionariaID = -1;
                }
        }
    },
        created() {
            this.clientesApi = new ClienteResource(this.$resource);
            this.concessApi = new ConcessionariaResource(this.$resource);
        }
    }
</script>

<style scoped>

    .cadForm {
        width: 100%;
    }

    .inputGroup {
        margin: 0 18px;
        width: 79%;
    }

    .side-by-side {
        display: grid;
        grid-template-columns: 5fr 2fr;
        width:87%;
        gap: 10px;

    }
    .side-by-side .inputGroup {
        margin: 0 auto;
    }


    .inputGroup label {
        display: block;
        text-align: left;
    }


    .inputGroup input {

        width: 100%;
        padding: 5px 10px 5px 10px;
        margin-bottom: 10px;
        text-align: center;
    }

    .dropdown select {
        width: 90%;
        margin: 10px auto;
        padding: 10px;
        text-align: center;
        margin-top: 0;
    }

    .separator {
        width: 100%;
    }

    .separator p {
        width: 100%;
        background-color: dimgray;
        color: white;}

    .buttonGroup, smallerButtonGroup {

        width: 100%;
    }

    .buttonGroup button {
        width: 90%;
        padding: 10px;
        margin: 10px auto;
        border-radius: 3px;
        text-align: center;
        font-size: 1.2em;
        cursor: pointer;
        background-color: white;
    }

    .smallerButtonGroup {
        display: flex;
        flex-direction: row;
        justify-content: space-between;
    }

    .smallerButtonGroup button {
        width: 30%;
        padding: 5px;
        margin: 5px auto;
        border-radius: 3px;
        text-align: center;
        font-size: 0.8em;
        cursor: pointer;
        background-color: white;
    }
    .button-default
      {
          border: 1px solid #4287f5;
          color: #4287f5;
      }

    .button-default:hover {
        background-color: #4287b5;
        color: white;
        transition: background-color 0.5s;
    }

    .button-danger
    {
        border: 1px solid palevioletred;
        color: palevioletred;
    }

    .button-danger:hover
    {
        background-color: palevioletred;
        color: white;
        transition: background-color 0.5s;
    }

    input:-webkit-autofill,
    input:-webkit-autofill:hover,
    input:-webkit-autofill:focus,
    textarea:-webkit-autofill,
    textarea:-webkit-autofill:hover,
    textarea:-webkit-autofill:focus,
    select:-webkit-autofill,
    select:-webkit-autofill:hover,
    select:-webkit-autofill:focus {
        border: 1px solid #4287b5;
        -webkit-text-fill-color: rgb(19, 33, 66);
        -webkit-box-shadow: 0 0 0px 1000px #b5e9ff inset;
        transition: background-color 5000s ease-in-out 0s;
        border-radius: 5px;
    }



</style>