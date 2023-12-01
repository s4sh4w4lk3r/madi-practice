<template>

    <table id="mainTable">
        <thead>
            <tr>
                <th>Id</th>
                <th>Маршрут</th>
                <th>Авиакомпания</th>
                <th>Добавлен в </th>
                <th>Обновлен в</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="item in airlineArray">
                <td contenteditable>{{ item.id }}</td>
                <td contenteditable>{{ item.name }}</td>
                <td contenteditable>{{ item.airline }}</td>
                <td contenteditable>{{ item.createdAt }}</td>
                <td contenteditable>{{ item.updatedAt }}</td>
            </tr>
        </tbody>
    </table>

    <div>
        <div>
            <input v-model="flight" type="text" class="flight-input" placeholder="Название рейса">
            <br>
            <input v-model="airline" type="text" clas="airline-input" placeholder="Название авиакомпании">
            <br>
            <button @click="fetch">Загрузить из бд</button>
            <br>
            <button @click="save">Добавить</button>
        </div>
    </div>
</template>

<script>
import axios from "axios";



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
        async save() {
            const dto = {
                Flight: this.flight,
                Airline: this.airline
            }

            await axios.post("http://localhost:5111/airlines/insert", dto);
            alert("OK");
        },

        async fetch() {
            this.airlineArray = (await axios.get("http://localhost:5111/airlines/get")).data;
        }
    }
}
</script>