// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 1.0.46
// 

using Colyseus.Schema;

public partial class State : Schema {
	[Type(0, "map", typeof(MapSchema<Player>))]
	public MapSchema<Player> players = new MapSchema<Player>();
}

