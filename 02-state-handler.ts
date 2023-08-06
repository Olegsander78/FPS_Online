import { Room, Client } from "colyseus";
import { Schema, type, MapSchema } from "@colyseus/schema";

export class Player extends Schema {
    @type("number")
    speed = 0;
    
    @type("number")
    pX = Math.floor(Math.random() * 50) - 25;

    @type("number")
    pY =0;
    
    @type("number")
    pZ = Math.floor(Math.random() * 50) - 25;
    
    @type("number")
    vX = 0;

    @type("number")
    vY = 0;

    @type("number")
    vZ = 0;

    @type("number")
    rX = 0;

    @type("number")
    rY = 0;

    @type("number")
    avY = 0;

    @type("number")
    cH = 2;
}

export class State extends Schema {
    @type({ map: Player })
    players = new MapSchema<Player>();

    something = "This attribute won't be sent to the client-side";

    createPlayer(sessionId: string, data:any) {
        const player = new Player();
        player.speed = data.speed;

        this.players.set(sessionId, player);
    }

    removePlayer(sessionId: string) {
        this.players.delete(sessionId);
    }

    movePlayer (sessionId: string, data: any) {
        const player = this.players.get(sessionId);
        player.pX = data.pX;
        player.pY = data.pY;
        player.pZ = data.pZ;
        player.vX = data.vX;
        player.vY = data.vY;
        player.vZ = data.vZ;
        player.rX = data.rX;        
        player.rY = data.rY;
        player.avY = data.avY;
        player.cH = data.cH;
    }
}

export class StateHandlerRoom extends Room<State> {
    maxClients = 4;

    onCreate (options) {
        console.log("StateHandlerRoom created!", options);

        this.setState(new State());

        this.onMessage("move", (client, data) => {
            //console.log("StateHandlerRoom received message from", client.sessionId, ":", data);
            this.state.movePlayer(client.sessionId, data);
        });

        this.onMessage("shoot", (client, data) => {
            this.broadcast("Shoot" , data, {except: client});
        })
    }

    onAuth(client, options, req) {
        return true;
    }

    onJoin (client: Client, data:any) {
        client.send("hello", "world");
        this.state.createPlayer(client.sessionId, data);
    }

    onLeave (client) {
        this.state.removePlayer(client.sessionId);
    }

    onDispose () {
        console.log("Dispose StateHandlerRoom");
    }

}

