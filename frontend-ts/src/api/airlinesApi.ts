import axios from "axios";

const host = "http://localhost:5111";

export class Airline {
    constructor(public readonly id: number, public flight: string, public airline: string, public createdAt?: string, public updatedAt?: string, public updateRequired: boolean = true) { }
}

export const airlinesApi = {
    async save(flightName: string, airlineName: string) {
        const dto = { Flight: flightName, Airline: airlineName }

        try {
            await axios.post(`${host}/airlines/insert`, dto);
            alert("OK");
        } catch (error) {
            console.error(error);
        }
    },

    async getAirlines() {
        try {
            const result: Airline[] = (await axios.get(`${host}/airlines/get`)).data as Airline[];
            result.forEach(e => e.updateRequired = false)
            return Promise.resolve(result);

        } catch (error) {
            console.error(error);
            return Promise.reject(error);
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

    async updateOne(id: number, flightName: string, airlineName: string) {
        const dto = { Id: id, Flight: flightName, Airline: airlineName };

        const result = await axios.patch(`${host}/airlines/update`, dto);
        if (result.status !== 200) {
            console.error(result.data);
        }
    },


    async updateMany(airlines: Airline[]) {

        airlines = airlines.filter(e => e.updateRequired === true);
        const airlinesDtos = airlines.map(e => ({
            Id: e.id,
            Flight: e.flight,
            Airline: e.airline
        }));

        const result = await axios.patch(`${host}/airlines/updatemany`, airlinesDtos);
        if (result.status !== 200) {
            console.error(result.data);
        }
    }
}