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
                <td><button type="button" @click="downloadById(item.id)">Download</button></td>
                <td><button type="button" @click="filesApi.deleteById(item.id)">Delete</button></td>
            </tr>
        </tbody>
    </table>
</main>
</template>

<script setup lang="ts">
import { api as filesApi, FileInfo } from '@/api/filesApi';
import { onMounted, ref } from 'vue';

const filesList = ref<FileInfo[]>();

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

function startBlobDownloading(blob: Blob): void {
    const objectUrl: string = URL.createObjectURL(blob);
    const a: HTMLAnchorElement = document.createElement('a') as HTMLAnchorElement;

    a.href = objectUrl;
    // a.download = ;
    document.body.appendChild(a);
    a.click();

    document.body.removeChild(a);
    URL.revokeObjectURL(objectUrl);
}

</script>

<style scoped>

main{
    background-color: black;
    color: white;
    height: 100vh;
    width: 100vw;
    background-color: black;
}

th,
td,
table {
    border: 1px solid;
}

th,
td {
    padding: 10px;
}

button{
    height: 33px;
    width: 100px;
    padding: 10px;
    border-radius: 5px;
    box-shadow: none;
    background-color: white;
    border-style: none;
}

</style>