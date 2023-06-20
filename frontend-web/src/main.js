import { createApp } from 'vue'
import App from './App.vue'
import { createRouter, createWebHistory } from 'vue-router'
import MaterialPage from '@/views/materials/MaterialPage.vue'
import MaterialGroupPage from './views/material_groups/MaterialGroupPage.vue'
import WarehousePage from '@/views/warehouses/WarehousePage.vue'
import CalculationUnitPage from '@/views/calculation_units/CalculationUnitPage.vue'
import clickOutside from './directive/clickoutside.js'
import esc from '@/directive/esc.js'

const app = createApp(App)

//Định nghĩa các router:
const routers = [
    {
        path: "/index",
        component: { template: "<div>Trang chủ</div>" }
    },
    {
        path: "/report",
        component: { template: "<div>Báo cáo</div>" }
    },
    {
        path: "/dictionary-material",
        component: MaterialPage
    },
    {
        path: "/dictionary-categorymaterial",
        component: MaterialGroupPage
    },
    {
        path: "/dictionary-stock",
        component: WarehousePage
    },
    {
        path: "/dictionary-unit",
        component: CalculationUnitPage
    },

]

//khởi tạo các router
const router = createRouter(
    {
        history: createWebHistory(),
        routes: routers
    }
)

//Khai báo một propertyGlobal
// app.config.globalProperties.commonJS = commonJS;
// app.config.globalProperties.MISAenum = MISAenum;

//Khai báo directive vào app
app.directive("outside", clickOutside);
app.directive("esc", esc);

//Đăng ký router vào app
app.use(router);
app.mount('#app')
