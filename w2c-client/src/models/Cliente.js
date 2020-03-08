export default class Cliente {

    constructor(nomeEmpresa='',
                ramoAtividade='',
                estado='',
                cidade='',
                concessionariaID=0,
                nomeResponsavel='',
                email='',
                telefone='')
    {
        this.nomeEmpresa = nomeEmpresa;
        this.ramoAtividade = ramoAtividade;
        this.estado = estado;
        this.cidade = cidade;
        this.concessionariaID = concessionariaID;
        this.nomeResponsavel = nomeResponsavel;
        this.email = email;
        this.telefone = telefone;
    }
}