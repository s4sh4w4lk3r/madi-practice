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
                        <td>{{ item.id }}</td>
                        <td>{{ item.lastname }}</td>
                        <td>{{ item.firstname }}</td>
                        <td>{{ item.patronymic }}</td>
                        <td>{{ item.email }}</td>
                        <td>{{ item.phone }}</td>
                        <td>{{ item.eduState }}</td>
                        <td>{{ item.gomadi }}</td>
                        <td>{{ item.year }}</td>
                        <td>{{ item.courses }}</td>
                        <td>{{ item.agree }}</td>
                        <td>{{ item.subscribe }}</td>
                        <td>{{ item.created_at }}</td>
                        <td>{{ item.updated_at }}</td>
                        <td>{{ item.deleted_at }}</td>
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
        json.root.forEach(element => {
            if (element.courses === true) { element.courses = "Да"; }
            else { element.courses = "Нет"; }

            if (element.agree === true) { element.agree = "Да"; }
            else { element.agree = "Нет"; }

            if (element.subscribe === true) { element.subscribe = "Да"; }
            else { element.subscribe = "Нет"; }

            if (element.gomadi === null) { element.gomadi = "Неизв."; }

            if (element.eduState === 0) { element.eduState = "Школа"; }
            if (element.eduState === 1) { element.eduState = "Колледж"; }
            if (element.eduState === 2) { element.eduState = "Вуз"; }

            this.items = json.root;
        });
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