import Vue from 'vue'
import Router from 'vue-router'
import Home from "@/views/Home.vue"
import YearsAdmin from "@/views/admin/YearsAdmin.vue"
import GradeCompany from "@/views/admin/GradeCompanyAdmin.vue"
import LocationAdmin from "@/views/admin/LocationAdmin.vue"
import HomeLanding from "@/views/landing/HomeLanding.vue"
import PlayerAdmin from "@/views/admin/PlayerAdmin.vue"
import SetAdmin from "@/views/admin/SetAdmin.vue"
import SportAdmin from "@/views/admin/SportAdmin.vue"
import TeamAdmin from "@/views/admin/TeamAdmin.vue"
import CardAdmin from "@/views/admin/CardAdmin.vue"

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
        ,
        {
            path: '/',
            name: 'Home',
            component: Home
        }
        ,
        {
            path: '/grade-company-admin',
            name: 'GradeCompany',
            component: GradeCompany
        }
        ,
        {
            path: '/location-admin',
            name: 'LocationAdmin',
            component: LocationAdmin
        },
        {
            path: '/player-admin',
            name: 'PlayerAdmin',
            component: PlayerAdmin
        },
        {
            path: '/set-admin',
            name: 'SetAdmin',
            component: SetAdmin
        },
        {
            path: '/sport-admin',
            name: 'SportAdmin',
            component: SportAdmin
        },
        {
            path: '/team-admin',
            name: 'TeamAdmin',
            component: TeamAdmin
        },
        {
            path: '/card-admin',
            name: 'CardAdmin',
            component: CardAdmin
        },
        {
            path: '/landing',
            name: 'HomeLanding',
            component: HomeLanding
        }
    ]
})