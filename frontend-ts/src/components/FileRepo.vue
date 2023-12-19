<template>
    <div>

    </div>
</template>

<script setup lang="ts">
import filesApi from '@/api/filesApi';
import { onMounted } from 'vue';

onMounted(()=>{
    doo();
});

async function doo() {
    const data = await filesApi.getFileById(4);
    downloadFile(data);
}


function downloadFile(blob: Blob): void {
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