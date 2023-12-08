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
            <tr v-for="item in airlineArray" @change="changeCell(item, 123, '')">
                <td>{{ item.id }}</td>
                <td contenteditable>{{ item.flight }}</td>
                <td contenteditable>{{ item.airline }}</td>
                <td>{{ new Date(Date.parse(item.createdAt!)).toLocaleString("RU-ru") }}</td>
                <td>{{ new Date(Date.parse(item.updatedAt!)).toLocaleString("RU-ru") }}</td>
                <td> <input type="button" @click="remove(item.id)" value="❌" required></td>
                <td> <input type="button" @click="fillFields(item)" value="🔧" required></td>
            </tr>
        </tbody>
    </table>

    <div>
        <div>
            <input v-model="flight" type="text" class="flight-input" placeholder="Название рейса">
            <br>
            <input v-model="airline" type="text" class="airline-input" placeholder="Название авиакомпании">
            <br>
            <button id="loadBtn" @click="load">Загрузить из бд</button>
            <br>
            <button id="saveBtn" @click="save">Добавить в базу данных</button>
            <br>
            <button id="updateBtn" @click="sendUpdates">Сохранить изменения</button>
            <br>
            <button id="exportBtn" @click="getExcel">Экспорт в Excel</button>
        </div>
    </div>
</template>

<script lang="ts">
import { Airline, airlinesApi, host as hostname } from "../api/airlinesApi"

type DataReturnType = { flight: string, airline: string, airlineArray: Airline[], idToUpdate: number };

export default {
    data(): DataReturnType {
        return {
            flight: "",
            airline: "",
            airlineArray: [],
            idToUpdate: 0
        };
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
            const result = await airlinesApi.getAirlines();
            this.airlineArray = result;
        },

        async remove(id: number) {
            await airlinesApi.deleteById(id);
            this.load();
        },

        fillFields(airline: Airline) {
            this.idToUpdate = airline.id;
            this.flight = airline.flight;
            this.airline = airline.airline;
            // Конечно не оч хорошо что оставил здесь воскл. знак.
            document.getElementById("saveBtn")!.style.visibility = "hidden";
            document.getElementById("loadBtn")!.style.visibility = "hidden";
            document.getElementById("updateBtn")!.style.visibility = "visible";
        },

        async sendUpdates() {
            // this.airlineArray.forEach(e=> {
            //     e.updateRequired = true;
            //     e.flight = "1111111111";
            // });
            await airlinesApi.updateOne(this.idToUpdate, this.flight, this.airline);
            this.flight = "";
            this.airline = "";
            document.getElementById("saveBtn")!.style.visibility = "visible";
            document.getElementById("loadBtn")!.style.visibility = "visible";
            document.getElementById("updateBtn")!.style.visibility = "hidden";
            await this.load();
        },


        getExcel(){
            window.open(`${hostname}/airlines/export/`, "_blank");
        }
        ,
        changeCell(changedData: Airline, id: number, row: string) {
            console.log(changedData, id, row);
            
        },
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