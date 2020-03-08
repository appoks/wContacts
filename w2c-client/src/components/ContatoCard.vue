<template>
    <div class="card-wrapper">
        <div class="contacts-info">
            <h3>{{cliente.nomeEmpresa.toUpperCase()}}</h3>
            <p class="ramo">{{cliente.ramoAtividade}}  /  {{cliente.cidade}} -  {{cliente.estado}}</p>
            <section class="ccliente">
                <h5>CONTATO TÉCNICO</h5>
                <p>{{cliente.nomeResponsavel}} - {{ cliente.telefone}}</p>
                <p>{{cliente.email}}</p>
                <p></p>
            </section>
            <span class="note">CONCESSIONÁRIA:  </span>
           <h3> {{concessionaria.nome}}</h3>

            <section class="cconcess">
                <h5>CONTATO TÉCNICO</h5>
                <p>{{concessionaria.contatoTecnico}} - {{concessionaria.telefone}}</p>
                <p>{{concessionaria.email}}</p>
            </section>
        </div>
        <section class="buttons-wrapper">
            <button type="button" @click="editContact">EDITAR CONTATO</button>

        </section>
    </div>
</template>

<script>
    //import Cliente from "../models/Cliente";
    //import Concessionaria from "../models/Concessionaria";

    export default {
        name: "ContatoCard",
        props: {
            cliente: {
                type: Object,
                required: true
            },
            concessionaria: {
                type: Object,
                required: true,
            }
        },
        methods: {
            editContact() {
                this.$emit('editRequest', this.cliente.id);
            },
        },
    }
</script>

<style scoped>
    .card-wrapper  {
        border: 1px solid darkgray;
        box-shadow: 0 0 14px 0 rgba(0, 0, 0, 0.2);
    }

    .contacts-info {
        margin: 5px;
        padding:  2px 15px;
    }
    .buttons-wrapper {
        display: flex;
        flex-direction: row;
        justify-content: space-around;
        padding: 0;
        margin: 0;
        padding: 10px;
        border-top: 1px solid lightgray ;
    }

    .buttons-wrapper button {
        display: inline-block;
        width: 98%;
        padding: 10px;
        border-radius: 3px;
        text-align: center;
        font-size: 1.2em;
        cursor: pointer;
        border: 1px solid #4287f5;
        background-color: white;
        color: #4287f5;
    }

    .buttons-wrapper button:hover {
        background-color: #4287b5;
        color: white;
        transition: background-color 0.5s;
    }

    .ccliente {
        border: 0.1px solid lightgray;
        padding: 0 10px;
        margin: 5px auto;
    }

    .ccliente h5, .cconcess h5 {
        text-align: left;
        margin: 5px 0;
        padding: 0;
        color: darkgray;
    }

    .cconcess {
        border: 0.1px solid lightgray;
        padding: 0 10px;
        margin: 5px auto;
    }

    h3 {
        line-height: 0;
        color: darkslategray;
    }

    .note{
        font-size: 12px;
        color: darkgray;
        font-weight: bold;
    }

    p {
        line-height: 0.85;
    }


</style>