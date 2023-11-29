<template>
    <div>
        <div>
            <input class="add-button" type="button" value="Добавить">
        </div>
        <br>
        <div>
            <input class="delete-button" type="button" value="Удалить">
        </div>
        <br>
        <div>
            <input class="update-button" type="button" value="Изменить">
        </div>
        <br>
        <br>
    </div>

    <div>
        <table>
            <caption>Анкетирование абитуриентов</caption>
            <thead>
                <tr>
                    <th> Id</th>
                    <th> Фамилия</th>
                    <th> Имя</th>
                    <th> Отчество</th>
                    <th> Email</th>
                    <th> Номер телефона</th>
                    <th> Образование</th>
                    <th> GoMadi</th>
                    <th> Год поступления</th>
                    <th> Курсы</th>
                    <th> Согласие</th>
                    <th> Подписка</th>
                    <th> Создано в </th>
                    <th> Обновлено в</th>
                    <th> Удалено в</th>
                </tr>
            </thead>
            <tbody>
                <template v-for="item in items">
                    <tr>
                        <td contenteditable>{{ item.id }}</td>
                        <td contenteditable>{{ item.lastname }}</td>
                        <td contenteditable>{{ item.firstname }}</td>
                        <td contenteditable>{{ item.patronymic }}</td>
                        <td contenteditable>{{ item.email }}</td>
                        <td contenteditable>{{ item.phone }}</td>
                        <td contenteditable>
                            <p v-if="item.eduState === 0">Школа</p>
                            <p v-if="item.eduState === 1">Колледж</p>
                            <p v-if="item.eduState === 2">ВУЗ</p>
                        </td>
                        <td contenteditable>
                            <p v-if="item.gomadi === null">Неизв.</p>
                        </td>
                        <td contenteditable>{{ item.year }}</td>
                        <td contenteditable>
                            <p v-if="item.courses === true">Да</p>
                            <p v-else>Нет</p>
                        </td>
                        <td contenteditable>
                            <p v-if="item.agree === true">Да</p>
                            <p v-else>Нет</p>
                        </td>
                        <td contenteditable>
                            <p v-if="item.subscribe === true">Да</p>
                            <p v-else>Нет</p>
                        </td>
                        <td contenteditable>{{ new Date( Date.parse(item.created_at)).toLocaleString("RU-ru") }} </td>
                        <td contenteditable>{{ new Date( Date.parse(item.updated_at)).toLocaleString("RU-ru") }}</td>
                        <td contenteditable>{{ new Date( Date.parse(item.deleted_at)).toLocaleString("RU-ru") }}</td>
                    </tr>
                </template>
            </tbody>
        </table>
    </div>
</template>

<script>
import json from "../assets/jopa123.json";
export default {
    data() {
        return {
            items: []
        }
    },
    mounted() {
        
        this.items = json.root;
    }
}
</script>

<style>
.add-button {
    margin-left: 70%;
}

.delete-button {
    margin-left: 70%;
}

.update-button {
    margin-left: 70%;
}
</style>