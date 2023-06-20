<template lang="">
    <div id="sidebar" v-if="isShowSideBar">
        <!-- <div id="side-bar" class="lite"> -->
        <div class="m-menu">
            <router-link to="/index" class="m-menu-item">
                <div class=" icon">
                    <i class="fas fa-home"></i>
                </div>
                <p class="sidebar-text">Tổng quan</p>
            </router-link>
            <router-link to="/report" class="m-menu-item space-between">
                <div class="flex">
                    <div class=" icon">
                        <i class="fas fa-chart-pie"></i>
                    </div>
                    <p class="sidebar-text">Báo cáo</p>
                </div>
                <div class="align-center">
                    <i class="far fa-arrow-alt-circle-down" style="font-size: 18px; padding-right: 4px;"></i>
                </div>
            </router-link>
            <div class="m-menu-item">
                <div class=" icon">
                    <i class="fas fa-shopping-cart"></i>
                </div>
                <p class="sidebar-text">Mua hàng</p>
            </div>
            <div class="m-menu-item">
                <div class="icon">
                    <i class="fas fa-warehouse" style="font-size: 16px"></i>
                </div>
                <p class="sidebar-text">Kho</p>
            </div>
            <div class="m-menu-item">
                <div class="icon">
                    <i class="fas fa-dollar-sign"></i>
                </div>
                <p class="sidebar-text">Quỹ tiền mặt</p>
            </div>
            <div class="m-menu-item">
                <div class="icon">
                    <i class="far fa-file-alt"></i>
                </div>
                <p class="sidebar-text">Quỹ tiền gửi</p>
            </div>
            <div class="m-menu-item">
                <div class="icon">
                    <i class="fas fa-file-invoice-dollar"></i>
                </div>
                <p class="sidebar-text">Chi phí</p>
            </div>
            <div class="m-menu-item">
                <div class="icon">
                    <i class="fas fa-gift"></i>
                </div>
                <p class="sidebar-text">Khuyến mại</p>
            </div>
            <div class="m-menu-item">
                <div class="icon">
                    <i class="fas fa-file-alt"></i>
                </div>
                <p class="sidebar-text">Thực đơn</p>
            </div>
            <div class="m-menu-item"  @click="showCategory">
                <div class="icon">
                    <i class="fas fa-bars"></i>
                </div>
                <p class="sidebar-text">Danh mục</p>
            </div>
            <div class="m-menu-item">
                <div class="icon">
                    <i class="fas fa-clipboard"></i>
                </div>
                <p class="sidebar-text">Hủy hóa đơn</p>
            </div>
            <div class="m-menu-item">
                <div class="icon">
                    <i class="fas fa-cog"></i>
                </div>
                <p class="sidebar-text">Thiết lập hệ thống</p>
            </div>
            <div class="m-menu-item">
                <div class="icon">
                    <i class="fas fa-th-large"></i>
                </div>
                <p class="sidebar-text">Ứng dụng</p>
            </div>
            <div class="m-menu-item space-between">
                <div class="flex">
                    <div class="icon">
                        <i class="fas fa-globe"></i>
                    </div>
                    <p class="sidebar-text">Bán hàng online</p>
                </div>
                <div class="align-center">
                    <i class="far fa-arrow-alt-circle-down" style="font-size: 18px; padding-right: 4px;"></i>
                </div>
            </div>
            <div class="m-menu-item space-between">
                <div class="flex">
                    <div class="icon">
                        <i class="fas fa-chart-line"></i>
                    </div>
                    <p class="sidebar-text">MISA Lomas</p>
                </div>
                <p style=" font-size: 11px; background-color: red; padding: 0 2px; border-radius: 4px">NEW</p>
            </div>
            <div class="m-menu-item space-between">
                <div class="flex">
                    <div class="icon">
                        <i class="far fa-question-circle"></i>
                    </div>
                    <p class="sidebar-text">Trợ giúp</p>
                </div>
                <div class="align-center">
                    <i class="far fa-arrow-alt-circle-down" style="font-size: 18px; padding-right: 4px;"></i>
                </div>
            </div>
            <div class="m-menu-item">
                <div class="icon">
                    <i class="far fa-user"></i>
                </div>
                <p class="sidebar-text">Thuê bao</p>
            </div>
        </div>
    </div>
</template>
<script>
export default {
    name: "SideBar",
    props: {

    },
    data() {
        return {
            isShowSideBar: true,
        }
    },
    methods: {
        showCategory() {
            this.$emit("showCategory");
        }
    }

}
</script>
<style scoped>
#sidebar {
    width: 230px;
    height: calc(100vh -55px);
    margin: 0px;
}

#sidebar p {
    color: #fff !important;
}

.m-menu {
    height: calc(100vh - 55px);
    overflow-y: scroll;
}

.m-menu-item {
    height: 40px;
    border-bottom: 1px solid #016087;
    border-top: 1px solid #038ac1;
    list-style-type: none;
    display: flex !important;
    align-items: center;
    text-decoration: none;
}

.m-menu-item .icon {
    width: 24px;
    height: 24px;
    margin: 0 12px;
    border-radius: 4px;
    line-height: 24px;
    text-align: center;
}

.m-menu-item i {
    color: #ffffff;
    font-size: 18px;
}

.m-menu-item:hover {
    background-color: #eb7728;
    cursor: pointer;
}

.m-menu-item--active,
.router-link-active {
    background-color: #0199fc !important;
}


/* Custom scroll bar */
/* width */
::-webkit-scrollbar {
    width: 5px;
}

::-webkit-scrollbar:horizontal {
    height: 5px;
}

/* Track */
::-webkit-scrollbar-track {
    background: #f1f1f100;
}

/* Handle */
::-webkit-scrollbar-thumb {
    background: #0414349b;
}

/* Handle on hover */
::-webkit-scrollbar-thumb:hover {
    cursor: pointer !important;
}
</style>