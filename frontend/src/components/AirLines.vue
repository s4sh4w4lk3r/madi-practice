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
            </tr>
        </thead>
        <tbody>
            <tr v-for="item in airlineArray">
                <td contenteditable>{{ item.id }}</td>
                <td contenteditable>{{ item.name }}</td>
                <td contenteditable>{{ item.airline }}</td>
                <td contenteditable>{{ new Date(Date.parse(item.createdAt)).toLocaleString("RU-ru") }}</td>
                <td contenteditable>{{ new Date(Date.parse(item.updatedAt)).toLocaleString("RU-ru") }}</td>
                <td> <input type="button" @click="remove(item.id)" value="X"></td>
            </tr>
        </tbody>
    </table>

    <div>
        <div>
            <input v-model="flight" type="text" class="flight-input" placeholder="Название рейса">
            <br>
            <input v-model="airline" type="text" clas="airline-input" placeholder="Название авиакомпании">
            <br>
            <button @click="load">Загрузить из бд</button>
            <br>
            <button @click="save">Добавить</button>
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
            airlineArray: null
        }
    },

    mounted() {
    },

    methods: {
        async save(){
            await airlinesApi.save.call(this, this.flight, this.airline);
            await this.load();
            this.flight = "";
            this.airline = "";
        },
        
        async load(){
            this.airlineArray = await airlinesApi.load();
            return Promise.resolve();
        },

        async remove(id) {
            await airlinesApi.deleteById(id);
            this.load();
        }
    }
}
</script>