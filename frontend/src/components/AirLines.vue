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
                <td>{{ item.name }}</td>
                <td>{{ item.airline }}</td>
                <td>{{ new Date(Date.parse(item.createdAt)).toLocaleString("RU-ru") }}</td>
                <td>{{ new Date(Date.parse(item.updatedAt)).toLocaleString("RU-ru") }}</td>
                <td> <input type="button" @click="remove(item.id)" value="❌"></td>
                <td> <input type="button" @click="fillFields(item.id, item.name, item.airline)" value="🔧"></td>
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

<script>
import airlinesApi from "../api/airlinesApi"

export default {
    data() {
        return {
            flight: "",
            airline: "",
            airlineArray: null,
            idToUpdate: 0
        }
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
            this.airlineArray = await airlinesApi.load();
            return Promise.resolve();
        },

        async remove(id) {
            await airlinesApi.deleteById(id);
            this.load();
        },

        async fillFields(id, flightName, airlineName){
            this.idToUpdate = id;
            this.flight = flightName;
            this.airline = airlineName;
            document.getElementById("saveBtn").style.visibility = "hidden";
            document.getElementById("loadBtn").style.visibility = "hidden";
            document.getElementById("updateBtn").style.visibility = "visible";

        },

        async sendUpdates(){
            await airlinesApi.update(this.idToUpdate, this.flight, this.airline);
            this.flight = "";
            this.airline = "";
            document.getElementById("saveBtn").style.visibility = "visible";
            document.getElementById("loadBtn").style.visibility = "visible";
            document.getElementById("updateBtn").style.visibility = "hidden";
            await this.load();
        }
    }
}
</script>