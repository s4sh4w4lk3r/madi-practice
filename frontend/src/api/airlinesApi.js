import axios from "axios";

export default {
    async save() {
        const dto = {
            Flight: this.flight,
            Airline: this.airline
        }

        try {
            await axios.post("http://localhost:5111/airlines/insert", dto);
            alert("OK");
        } catch (error) {
            console.error(error);
        }
    },

    async load() {
        try {
            this.airlineArray = (await axios.get("http://localhost:5111/airlines/get")).data;

        } catch (error) {
            console.error(error);
        }
    }
}