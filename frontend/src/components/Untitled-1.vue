<template>
    <div class="tittle-container">
        <div class="container">
            <H2 class="wel">
                Крутая таблица
            </H2>
            <div class="bb">
                <div class="block">
                    <button class="buttonAdd" v-on:click="addItems">добавить</button>
                    <table class="table">
                        <thead>
                            <tr>
                                <th>id</th>
                                <th>lastname</th>
                                <th>firstname</th>
                                <th>patronymic</th>
                                <th>email</th>
                                <th>phone</th>
                                <th>eduState</th>
                                <th>gomadi</th>
                                <th>year</th>
                                <th>courses</th>
                                <th>agree</th>
                                <th>subscribe</th>
                                <th>created_at</th>
                                <th>updated_at</th>
                                <th>deleted_at</th>
                                <th>Удалить</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="item in items " :key="item.id">
                                <td>{{ item.id }}</td>
                                <td>{{ item.lastname }}</td>
                                <td>{{ item.firstname }}</td>
                                <td>{{ item.patronymic }}</td>
                                <td>{{ item.email }}</td>
                                <td>{{ item.phone }}</td>
                                <td>{{ item.eduState }}</td>
                                <td>{{ item.gomadi }}</td>
                                <td>{{ item.year }}</td>
                                <td>
                                    <p v-if="item.courses">да</p>
                                    <p v-else>нет</p>
                                </td>
                                <td>
                                    <p v-if="item.agree">да</p>
                                    <p v-else>нет</p>
                                </td>
                                <td>
                                    <p v-if="item.subscribe === true">да</p>
                                    <p v-else>нет</p>
                                </td>
                                <td>{{ item.created_at }}</td>
                                <td>{{ item.updated_at }}</td>
                                <td>{{ item.deleted_at }}</td>
                                <td>
                                    <button class="deleteBtn" v-on:click="deleteItems(item.id)">удалить</button>
                                </td>


                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    name: "Test",
    data() {
        return {
            items: []
        }
    },
    mounted() {
        const json = require('./TestTable.json');
        this.items = json;
    },
    methods: {
        deleteItems(itemId) {
            const index = this.items.findIndex(item => item.id === itemId);
            this.items.splice(index, 1);
        },
        addItems() {
            const newId = this.randomId(4, 25);
            const newItem = {
                id: newId,
                lastname: '',
                firstname: '',
                patronymic: '',
                email: '',
                phone: '',
            };
            this.items.push(newItem)
        },
        randomId(min, max) {
            return Math.round(min - 0.5 + Math.random() * (max - min + 1))
        }
    }

}
</script>

<style>
.bb {
    display: flex;
    align-items: center;
    justify-content: cent
}
</style>