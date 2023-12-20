import kyBase, { type Options } from "ky";
import type { KyHeadersInit } from "node_modules/ky/distribution/types/options";
const ky = kyBase.create({ prefixUrl: "http://localhost:5111/filerepo/" });

export class FileInfo {
    constructor(public id: number, public filename: string,
        public path: string, public MimeType: string) { }
}

export const api = {
    async getList() {
        return await ky.get("getall").json<FileInfo[]>(); 
    },

    async getFileById(id: number) {
        return await ky.get(`get?id=${id}`).blob(); 
    },

    async deleteById(id: number) {
        return await ky.delete(`del?id=${id}`);
    },

    async uploadFile(file:File, dirToSave = ""){
        const formData = new FormData();
        formData.append("formFile", file);
        formData.append("dirToSave", dirToSave)
        const options : Options = {body: formData};
        await ky.post("add", options);
    }
}