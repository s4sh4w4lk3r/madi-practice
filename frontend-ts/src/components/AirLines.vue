<template>
    <table id="mainTable">
        <thead>
            <tr>
                <th>Id</th>
                <th>Маршрут</th>
                <th>Авиакомпания</th>
                <th>Добавлен в </th>
                <th>Обновлен в</th>
                <th></th>
                <th></th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="item in airlineArray">
                <td>{{ item.id }}</td>
                <td>{{ item.flight }}</td>
                <td>{{ item.airline }}</td>
                <td>{{ new Date(Date.parse(item.createdAt!)).toLocaleString("RU-ru") }}</td>
                <td>{{ new Date(Date.parse(item.updatedAt!)).toLocaleString("RU-ru") }}</td>
                <td> <input type="button" @click="remove(item.id)" value="❌"></td>
                <td> <input type="button" @click="fillFields(item.id, item.flight, item.airline)" value="🔧"></td>
            </tr>
        </tbody>
    </table>

    <div>
        <div>
            <input v-model="flight" type="text" class="flight-input" placeholder="Название рейса">
            <br>
            <input v-model="airline" type="text" clas="airline-input" placeholder="Название авиакомпании">
            <br>
            <button id="loadBtn" @click="load">Загрузить из бд</button>
            <br>
            <button id="saveBtn" @click="save">Добавить в базу данных</button>
            <br>
            <button id="updateBtn" @click="sendUpdates">Сохранить изменения</button>
        </div>
    </div>
</template>

<script lang="ts">
import airlinesApi, {Airline} from "../api/airlinesApi"


export default {
    data() {
        const da : {flight:string, airline: string, airlineArray: Airline[], idToUpdate: number} = {
            flight: "",
            airline: "",
            airlineArray: [],
            idToUpdate : 0
        };

        return da;
    },

    mounted() {
    },

    methods: {
        async save() {
            await airlinesApi.save.call(this, this.flight, this.airline);
            await this.load();
            this.flight = "";
            this.airline = "";
        },

        async load() {
            const result = await airlinesApi.load() as Airline[];
            this.airlineArray = result;
            return Promise.resolve();
        },

        async remove(id: number) {
            await airlinesApi.deleteById(id);
            this.load();
        },

        async fillFields(id: number, flightName: string, airlineName: string) {
            this.idToUpdate = id;
            this.flight = flightName;
            this.airline = airlineName;
            document.getElementById("saveBtn")!.style.visibility = "hidden";
            document.getElementById("loadBtn")!.style.visibility = "hidden";
            document.getElementById("updateBtn")!.style.visibility = "visible";

        },

        async sendUpdates() {
            await airlinesApi.update(this.idToUpdate, this.flight, this.airline);
            this.flight = "";
            this.airline = "";
            document.getElementById("saveBtn")!.style.visibility = "visible";
            document.getElementById("loadBtn")!.style.visibility = "visible";
            document.getElementById("updateBtn")!.style.visibility = "hidden";
            await this.load();
        }
    }
}
</script>

<style>
table,
th,
td {
    border: 1px solid;
}

table {
    width: 65%;
}

th,
td {
    padding: 15px;
    text-align: center;
}

tr:nth-child(even) {
    background-color: #423733;
}

body {
    background-color: #302828;
    color: darksalmon;
}

button {
    background-color: #34221b;
    color: darksalmon;
    border-radius: 9px;
    margin-top: 5px;
}

input[type="text"] {
    background-color: #7c6666;
    color: #e9967a;
    margin-top: 5px;
}

::placeholder {
    color: #ffaf86;
    opacity: 1;
    /* Firefox */
}
</style>