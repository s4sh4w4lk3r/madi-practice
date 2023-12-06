import axios from "axios";

const host = "http://localhost:5111";

export class Airline {

    constructor(public id: number, public flight: string, public airline: string, public createdAt?: string, public updatedAt?: string) { }
}

export default {
    async save(flightName: string, airlineName: string) {
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
            const result = (await axios.get(`${host}/airlines/get`)).data as Airline[];
            return result;

        } catch (error) {
            console.error(error);
        }
    },

    async deleteById(id: number) {
        try {
            const result = await axios.delete(`${host}/airlines/delete?id=${id}`);
            if (result.status !== 200) console.error(result.data);
        } catch (error) {
            console.error(error);
        }
    },

    async update(id: number, flightName: string, airlineName: string) {
        const dto = { Id: id, Flight: flightName, Airline: airlineName };

        const result = await axios.patch(`${host}/airlines/update`, dto);
        if (result.status !== 200) {
            console.error(result.data);
        }
    }
}