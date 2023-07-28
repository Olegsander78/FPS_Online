import { Room, Client } from "colyseus";
import { Schema, type, MapSchema } from "@colyseus/schema";

export class Player extends Schema {
    @type("number")
    x = Math.floor(Math.random() * 50) - 25;

    @type("number")
    y = Math.floor(Math.random() * 50) - 25;

    @type("number")
    vx = 0;

    @type("number")
    vy = 0;
}

export class State extends Schema {
    @type({ map: Player })
    players = new MapSchema<Player>();

    something = "This attribute won't be sent to the client-side";

    constructor() {
        super();
        this.players = new MapSchema<Player>();
    }

    createPlayer(sessionId: string) {
        this.players.set(sessionId, new Player());
    }

    removePlayer(sessionId: string) {
        this.players.delete(sessionId);
    }

    movePlayer (sessionId: string, data: any) {
        if (data.x) {
            this.players.get(sessionId).x = data.x;
        } 
        
        if (data.y) {
            this.players.get(sessionId).y = data.y;
        }
        
        if (data.vx) {
            this.players.get(sessionId).vx = data.vx;
        }
        
        if (data.vy) {
            this.players.get(sessionId).vy = data.vy;
        }
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

        this.onMessage("hello", (client, data) => {
            console.log("Received hello message from client", client.sessionId);
        });
    }

    onAuth(client, options, req) {
        return true;
    }

    onJoin (client: Client) {
        client.send("hello", "world");
        this.state.createPlayer(client.sessionId);
    }

    onLeave (client) {
        this.state.removePlayer(client.sessionId);
    }

    onDispose () {
        console.log("Dispose StateHandlerRoom");
    }
}
