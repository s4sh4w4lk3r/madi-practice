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
            <input v-model="airlineName" type="text" class="airline-input" placeholder="Название авиакомпании">
            <br>
            <button id="loadBtn" @click="load">Загрузить из бд</button>
            <br>
            <!-- <button id="saveBtn" @click="save">Добавить в базу данных</button>
            <br> -->
            <button id="updateBtn" @click="sendUpdates">Сохранить изменения</button>
            <br>
        </div>

        <div class="excelButtons">
            <button id="exportBtn" @click="getExcel">Экспорт в Excel</button>
            <br>
            <input id="importBtn" type="file" name="importBtin" @change="importExcel">
        </div>

    </div>
</template>

<script lang="ts" setup>
import { ref } from 'vue'
import { Airline, airlinesApi, host as hostname } from "../api/airlinesApi"

type DataReturnType = { flight: string, airline: string, airlineArray: Airline[], idToUpdate: number };

let flight = "";
let airlineName = "";
let airlineArray: Airline[] = [];
let idToUpdate = 0;
// async function save() {
//     await airlinesApi.save.call(this, flight, airlineName);
//     await load();
//     flight = "";
//     airlineName = "";
// }

async function load() {
    airlineArray = await airlinesApi.getAirlines();
}

async function remove(id: number) {
    await airlinesApi.deleteById(id);
    await load();
}

function fillFields(airline: Airline) {
    idToUpdate = airline.id;
    flight = airline.flight;
    airlineName = airline.airline;
    // Конечно не оч хорошо что оставил здесь воскл. знак.
    document.getElementById("saveBtn")!.style.visibility = "hidden";
    document.getElementById("loadBtn")!.style.visibility = "hidden";
    document.getElementById("updateBtn")!.style.visibility = "visible";
}

async function sendUpdates() {
    // this.airlineArray.forEach(e=> {
    //     e.updateRequired = true;
    //     e.flight = "1111111111";
    // });
    await airlinesApi.updateOne(idToUpdate, flight, airlineName);
    flight = "";
    airlineName = "";
    document.getElementById("saveBtn")!.style.visibility = "visible";
    document.getElementById("loadBtn")!.style.visibility = "visible";
    document.getElementById("updateBtn")!.style.visibility = "hidden";
    await load();
}


function getExcel() {
    window.open(`${hostname}/airlines/export/`, "_blank");
}

function changeCell(changedData: Airline, id: number, row: string) {
    console.log(changedData, id, row);

}

async function importExcel(event: Event) {
    const target = event.target as HTMLInputElement;
    const file: File = (target.files as FileList)[0];
    await airlinesApi.importExcel(file);
    alert("OK");
    await load();
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