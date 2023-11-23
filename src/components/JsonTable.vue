<template>
    <div>
        <table>
            <thead>
                <tr>
                    <th> Id</th>
                    <th> Lastname</th>
                    <th> Firstname</th>
                    <th> Patronymic</th>
                    <th> Email</th>
                    <th> Phone</th>
                    <th> EduState</th>
                    <th> GoMadi</th>
                    <th> Year</th>
                    <th> Courses</th>
                    <th> Agree</th>
                    <th> Subscribe</th>
                    <th> Created At</th>
                    <th> Updated At</th>
                    <th> Deleted At</th>
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
        this.items = json.root;
    }
}
</script>`