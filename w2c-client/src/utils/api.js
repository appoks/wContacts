export class ClienteResource {

    constructor(resource) {

        this._resource = resource('api/clientes{/id}');
    }

    list() {
        return this._resource.query().then(res => res.json());
    }

    add(cliente) {
        return this._resource.save(cliente).then( res => res.json());
    }

    remove(id) {
        return this._resource.delete( { id } );
    }

    get(id) {
        return this._resource.get( { id } ).then( res => res.json());
    }

    edit(cliente) {
        return this._resource.update({ id: cliente.id }, cliente);
    }
}

export class ConcessionariaResource {

    constructor(resource) {

        this._resource = resource('api/concessionarias{/id}');
    }

    list() {
        return this._resource.query().then(res => res.json());
    }

    add(concessionaria) {
        return this._resource.save(concessionaria).then(res => res.json());
    }

    remove(id) {
        return this._resource.delete( { id } );
    }

    get(id) {
        return this._resource.get( { id } ).then( res => res.json());
    }

    edit(concessionaria) {
        return this._resource.update({ id: concessionaria.id }, concessionaria).then( res => res.json());
    }
}