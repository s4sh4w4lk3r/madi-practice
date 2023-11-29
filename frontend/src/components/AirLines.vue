<template>
    <div>
        <input v-model="flight" type="text" class="flight-input" placeholder="Название рейса">
        <br>
        <input v-model="airline" type="text" clas="airline-input" placeholder="Название авиакомпании">
        <br>
        <button @click="save">Сохранить</button>
    </div>
</template>

<script>
import axios from "axios";


export default {
    data() {
        return {
            flight: "",
            airline: ""
        }
    },
    mounted() {

    },
    methods: {
        save() {
            const dto = {
                Flight: this.flight,
                Airline: this.airline
            }

            axios.post("http://localhost:5111/airlines/insert", dto);
        }
    }
}
</script>