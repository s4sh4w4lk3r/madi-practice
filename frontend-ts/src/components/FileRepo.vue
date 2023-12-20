<template>
    <main>
        <table>
            <thead>
                <tr>
                    <th>Id</th>
                    <th>Имя</th>
                    <th>Путь</th>
                    <th></th>
                    <th></th>
                </tr>
            </thead>

            <tbody>
                <tr v-for="item in filesList">
                    <td>{{ item.id }}</td>
                    <td>{{ item.filename }}</td>
                    <td>{{ item.path }}</td>
                    <td><button class="table-button" type="button" @click="downloadById(item.id)">Download</button></td>
                    <td><button class="table-button" type="button" @click="deleteByIdAndPrint(item.id)">Delete</button></td>
                </tr>
            </tbody>
        </table>

        <div class="uploadDiv">
            <input id="fileInput" type="file" />
            <input class="textbox" v-model="dirToSave" type="text" placeholder="Введите дерикторию">
            <button type="button" @click="getAndUploadFile">Отправить</button>
        </div>
    </main>
</template>

<script setup lang="ts">
import { api as filesApi, FileInfo } from '@/api/filesApi';
import { onMounted, ref } from 'vue';

const filesList = ref<FileInfo[]>([]);
const dirToSave = ref<string>("");

onMounted(() => {
    fetchAndPrint();
});

async function downloadById(id: number) {
    const data = await filesApi.getFileById(id);
    startBlobDownloading(data);
}

async function fetchAndPrint() {
    filesList.value = await filesApi.getList();
}

async function deleteByIdAndPrint(id: number) {
    await filesApi.deleteById(id);
    await fetchAndPrint();
}

function startBlobDownloading(blob: Blob): void {
    const objectUrl: string = URL.createObjectURL(blob);
    const a: HTMLAnchorElement = document.createElement('a') as HTMLAnchorElement;

    a.href = objectUrl;
    document.body.appendChild(a);
    a.click();

    document.body.removeChild(a);
    URL.revokeObjectURL(objectUrl);
}

async function getAndUploadFile() {
    const el = document.getElementById("fileInput") as HTMLInputElement;
    const file: File = (el.files as FileList)[0];
    await filesApi.uploadFile(file, dirToSave.value);
    await fetchAndPrint();
    alert("OK");
    location.reload();
}
</script>

<style scoped>
@import url('https://fonts.googleapis.com/css2?family=Roboto&display=swap');

main {  
    color: white;
    height: 100vh;
    width: 100vw;
    display: flex;
    flex-direction: column;
    align-items: center;
    background-color: black
}



table {
    border: 1px solid;
    font-family: 'Roboto', sans-serif;
    border-radius: 5px;
    min-width: 35%;
    margin-top: 15px
}

th,
td {
    padding: 10px;
}

button {
    height: 33px;
    width: 100px;
    padding: 10px;
    border-radius: 5px;
    box-shadow: none;
    background-color: white;
    border-style: none;
}

button:hover {
    background-color: gray;
}

.uploadDiv {
    display: flex;
    flex-direction: column;
    gap: 5px;
    width: 300px;
    padding: 5px;
    margin: 10px;
    .textbox {
        width: 250px;
        padding: 9px;
        border-radius: 5px;
        box-shadow: none;
        background-color: white;
        border-style: none;
    }
    
    border-style: solid;
    border-width: 1px;
    border-color: white;
    border-radius: 5px;
}
</style>