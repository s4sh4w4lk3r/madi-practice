import kyBase from "ky";
const ky = kyBase.create({ prefixUrl: "http://localhost:5111/filerepo/" });

class FileInfo {
    constructor(public id: number, public filename: string,
        public path: string, public MimeType: string) { }
}

export default {
    async getList() {
        return await ky.get("getall").json<FileInfo[]>(); 
    },

    async getFileById(id: number) {
        return await ky.get(`get?id=${id}`).blob(); 
    }
}