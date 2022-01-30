import Vue from 'vue'
import Router from 'vue-router'
import Home from "@/views/Home.vue"

Vue.use(Router)

export default new Router({
    routes: [
        {
            path: "/",
            //redirect: {
                name: "Home",
            //},
            component: Home
        },
        {
            path: '/page1',
            name: 'Page1',
            component: Home
        },
        {
            path: '/page2',
            name: 'Page2',
            component: Home
        }
    ]
})