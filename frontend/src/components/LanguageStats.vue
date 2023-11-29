<template>
    <Bar id="my-chart-id" :options="chartOptions" :data="chartData" />
</template>
<script>
import json from "../assets/mostUsedLanguages.json";
import { Bar } from "vue-chartjs";
import { Chart as ChartJS, Title, Tooltip, Legend, BarElement, CategoryScale, LinearScale } from 'chart.js';

ChartJS.register(Title, Tooltip, Legend, BarElement, CategoryScale, LinearScale);

export default {
    name: 'BarChart',
    components: { Bar },

    data() {
        return {
            chartData: {
                labels: json.languages.map(e => e.name),
                datasets: [
                    {
                        data: json.languages.map(e => e.percentage),
                        label: "Количество коммитов %",
                        backgroundColor: ['#C28535', '#8AAE56', '#800020', '#00FFCC', '#FF97C0', '#FFC61A', '#66CCCC', '#4A2AD2', '#27D128', '#736372'],
                    }
                ],
                backgroundColor: '#f87979',
            },
            chartOptions: {
                responsive: true
            }
        }
    },

    mounted() {
        this.items = json.languages;
    }
}
</script>