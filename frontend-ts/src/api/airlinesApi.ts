import axios from "axios";

export const host = "http://localhost:5111";

const api = axios.create({baseURL: host})
export class Airline {
    constructor(public readonly id: number, public flight: string, public airline: string, public createdAt?: string, public updatedAt?: string, public updateRequired: boolean = true) { }
}

export const airlinesApi = {
    async save(flightName: string, airlineName: string) {
        const dto = { Flight: flightName, Airline: airlineName }

        try {
            await api.post(`/airlines/insert`, dto);
            alert("OK");
        } catch (error) {
            console.error(error);
        }
    },

    async getAirlines() {
        try {
            const result: Airline[] = (await api.get(`/airlines/get`)).data as Airline[];
            result.forEach(e => e.updateRequired = false)
            return Promise.resolve(result);

        } catch (error) {
            console.error(error);
            return Promise.reject(error);
        }
    },

    async deleteById(id: number) {
        try {
            const result = await api.delete(`/airlines/delete?id=${id}`);
            if (result.status !== 200) console.error(result.data);
        } catch (error) {
            console.error(error);
        }
    },

    async updateOne(id: number, flightName: string, airlineName: string) {
        const dto = { Id: id, Flight: flightName, Airline: airlineName };

        const result = await api.patch(`/airlines/update`, dto);
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

        const result = await api.patch(`/airlines/updatemany`, airlinesDtos);
        if (result.status !== 200) {
            console.error(result.data);
        }
    },

    async importExcel(file: File) {
        const formData = new FormData();
        formData.append("formFile", file);
        const result = await api.post(`/airlines/import`, formData, {
            headers: {
                'Content-Type': 'multipart/form-data'
            }
        });

        if (result.status !== 200) {
            return Promise.resolve();
        }
        else Promise.resolve(result.data)
    }
}