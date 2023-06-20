<template>
    <TheHeader @showSideBar="adjustSideBar"></TheHeader>
    <div id="main" class="flex">
        <SideBar ref="sidebar" @showCategory="adjustCategory"></SideBar>
        <MainContent ref="content"></MainContent>
    </div>
</template>
<script>

import TheHeader from './components/layout/TheHeader.vue';
import SideBar from './components/layout/SideBar.vue';
import MainContent from './components/layout/MainContent.vue';

export default {
    name: "App",
    components: {
        TheHeader, SideBar, MainContent
    },
    updated() {

    },
    data() {
        return {

        }
    },
    methods: {
        /**
         * Hàm click đóng mở sidebar
         * params: 
         * Author: CVViệt ()
         * Date: 27/05/2023
         */
        adjustSideBar() {
            this.$refs.sidebar.isShowSideBar = !this.$refs.sidebar.isShowSideBar;
            this.$refs.content.isCloseSideBar = !this.$refs.content.isCloseSideBar;
            this.$refs.content.isShowCategory = false;
        },
        /**
         * Hàm click đóng mở category
         * params: 
         * Author: CVViệt ()
         * Date: 27/05/2023
         */
        adjustCategory() {
            this.$refs.content.isShowCategory = !this.$refs.content.isShowCategory;
            // console.log(this.$refs.content.isShowCategory);
        }

    }
}
</script>
<style scoped>
@import url(../src/css/main.css);
</style>
