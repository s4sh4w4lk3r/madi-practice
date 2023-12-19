<template>
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
                <td><button type="button" value="Download"></button></td>
                <td><button type="button" value="Delete"></button></td>
            </tr>
        </tbody>

    </table>
</template>

<script setup lang="ts">
import {api as filesApi, FileInfo} from '@/api/filesApi';
import { onMounted, ref } from 'vue';

const filesList = ref<FileInfo[]>();

onMounted(() => {
    fetchAndPrint();
});

async function download(id: number) {
    const data = await filesApi.getFileById(id);
    startBlobDownloading(data);
}

async function fetchAndPrint(){    
    filesList.value = await filesApi.getList()
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

</style>