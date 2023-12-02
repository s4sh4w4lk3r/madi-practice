import axios from "axios";

const host = "http://localhost:5111";

export default {
    async save(flightName, airlineName) {
        const dto = { Flight: flightName, Airline: airlineName }

        try {
            await axios.post(`${host}/airlines/insert`, dto);
            alert("OK");
        } catch (error) {
            console.error(error);
        }
    },

    async load() {
        try {
            return (await axios.get(`${host}/airlines/get`)).data;

        } catch (error) {
            console.error(error);
        }
    },

    async deleteById(id) {
        try {
            const result = await axios.delete(`${host}/airlines/delete?id=${id}`);
            if (result.status !== 200) console.error(result.data);
        } catch (error) {
            console.error(error);
        }
    },

    async update(id, flightName, airlineName) {
        const dto = { Id: id, Flight: flightName, Airline: airlineName };

        const result = await axios.patch(`${host}/airlines/update`, dto);
        if (result.status !== 200) {
            console.error(result.data);
        }
    }
}