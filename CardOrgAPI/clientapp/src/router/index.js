import Vue from 'vue'
import Router from 'vue-router'
import Home from "@/views/Home.vue"
import YearsAdmin from "@/views/admin/YearsAdmin.vue"

Vue.use(Router)

export default new Router({
    routes: [
        {
            path: '/years-admin',
            name: 'YearsAdmin',
            component: YearsAdmin
        },
        {
            path: '/home',
            name: 'Home',
            component: Home
        }
    ]
})