<template>
    <div id="header">
        <div class="left-header">
            <div class="flex header-logo">
                <div class="icon-left-header" @click="btnClickShowSideBar">
                    <i class="fas fa-bars" style="font-size: 20px;"></i>
                </div>
                <div class="text-left-header">CUKCUK</div>
            </div>
            <GDropList :textInput="stores.textInput" type="header" :items="stores.storeArray">
            </GDropList>
        </div>
        <div class="right-header">
            <GDropList :textInput="languages.textInput" type="header" :items="languages.languageArray"></GDropList>
            <div class="align-center" style="padding: 0 12px 0 24px;">
                <i class="fas fa-bullhorn"></i>
            </div>
            <div class="align-center" style="padding: 0 12px;">
                <i class="fas fa-globe-asia"></i>
            </div>
            <div class="align-center" style="width: 90px;padding: 0 24px 0 32px;">
                <i class="fas fa-user-circle" style="padding: 0 4px;"></i>
                <p>Việt</p>
                <i class="fas fa-caret-down" style="padding-left: 12px; font-size: 13px; "></i>
            </div>
        </div>
    </div>
</template>
<script>

import resource from '@/resources/resource';
import GDropList from '@/components/globalComponents/GDroplist.vue'

export default {
    name: "TheHeader",
    components: { GDropList },
    props: {
        stores: {
            default: {
                textInput: resource.stores.text,
                storeArray: resource.stores.value
            }
        },
        languages: {
            default: {
                textInput: resource.languages.text,
                languageArray: resource.languages.value
            }
        }
    },
    data() {
        return {

        }
    },
    methods: {
        /**
         * btn đóng mở sidebar
         * params: 
         * Author: CVViệt ()
         * Date: 25/05/2023
         */
        btnClickShowSideBar() {
            this.$emit("showSideBar");
        }
    },

}
</script>
<style scoped>
#header {
    /* position: relative; */
    height: 55px;
    color: #ffffff;
    padding: 12px;
    border-bottom: 1px solid #016087;
    display: flex;
    justify-content: space-between;
    align-items: center;
}

#header i {
    cursor: pointer;
    font-size: 20px;
    color: #ffffff;
}

.left-header {
    display: flex;
    align-items: center;
}

.right-header {
    display: flex;
    align-items: center;
}

.header-logo {
    width: 232px;
    display: flex;
    align-items: center;
}

.header-logo .icon-left-header {
    cursor: pointer;
    margin: 12px 8px 8px 8px;
}

.header-logo .text-left-header {
    font-size: 30px;
    font-weight: bolder;
}
</style>