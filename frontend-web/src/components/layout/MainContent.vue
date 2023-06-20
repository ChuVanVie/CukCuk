<template>
    <div id="main-content" :class="{ 'full-width-content': isCloseSideBar }">
        <router-view></router-view>
        <div id="catergory-mask" class="flex" v-outside="clickOutside" v-if="isShowCategory">
            <div class="category-col flex-c">
                <div class="menu flex-c">
                    <div class="menu-title items-center">
                        <i class="fas fa-glass-cheers"></i>
                        <p>THỰC ĐƠN</p>
                    </div>
                    <div class="menu-item items-center">
                        <div class="item-node items-center">
                            <div class="line-1"></div>
                            <div class="line-2"></div>
                        </div>
                        <div class="item-content">Thực đơn</div>
                    </div>
                    <div class="menu-item items-center">
                        <div class="item-node items-center">
                            <div class="line-1"></div>
                            <div class="line-2"></div>
                        </div>
                        <div class="item-content">Nhóm thực đơn</div>
                    </div>
                    <div class="menu-item items-center">
                        <div class="item-node items-center">
                            <div class="line-1"></div>
                            <div class="line-2"></div>
                        </div>
                        <div class="item-content">Sở thích phục vụ</div>
                    </div>
                    <div class="menu-item items-center">
                        <div class="item-node items-center">
                            <div class="line-1"></div>
                            <div class="line-2"></div>
                        </div>
                        <div class="item-content">Nhóm sở thích phục vụ</div>
                    </div>
                    <div class="menu-item items-center">
                        <div class="item-node items-center">
                            <div class="line-1"></div>
                            <div class="line-2"></div>
                        </div>
                        <div class="item-content">Dịch vụ tính tiền theo thời gian</div>
                    </div>
                    <div class="menu-item flex">
                        <div class="item-node">
                            <div class="line-1" style="height: 15px;"></div>
                            <div class="line-2" style="margin-left: 13px;"></div>
                        </div>
                        <div class="item-content">Thuế suất</div>
                    </div>
                </div>
                <div class="menu flex-c">
                    <div class="menu-title items-center">
                        <i class="fas fa-users"></i>
                        <p>KH, NCC, NHÂN VIÊN</p>
                    </div>
                    <div class="menu-item items-center">
                        <div class="item-node items-center">
                            <div class="line-1"></div>
                            <div class="line-2"></div>
                        </div>
                        <div class="item-content">Nhân viên</div>
                    </div>
                    <div class="menu-item items-center">
                        <div class="item-node items-center">
                            <div class="line-1"></div>
                            <div class="line-2"></div>
                        </div>
                        <div class="item-content">Khách hàng</div>
                    </div>
                    <div class="menu-item items-center">
                        <div class="item-node items-center">
                            <div class="line-1"></div>
                            <div class="line-2"></div>
                        </div>
                        <div class="item-content">Nhóm khách hàng</div>
                    </div>
                    <div class="menu-item items-center">
                        <div class="item-node items-center">
                            <div class="line-1"></div>
                            <div class="line-2"></div>
                        </div>
                        <div class="item-content">Nhà cung cấp</div>
                    </div>
                    <div class="menu-item items-center">
                        <div class="item-node items-center">
                            <div class="line-1"></div>
                            <div class="line-2"></div>
                        </div>
                        <div class="item-content">Nhóm nhà cung cấp</div>
                    </div>
                    <div class="menu-item flex">
                        <div class="item-node">
                            <div class="line-1" style="height: 15px;"></div>
                            <div class="line-2" style="margin-left: 13px;"></div>
                        </div>
                        <div class="item-content">Đối tác giao hàng/Kênh bán hàng</div>
                    </div>
                </div>
            </div>
            <div class="category-col flex-c">
                <div class="menu flex-c">
                    <div class="menu-title items-center">
                        <i class="fas fa-shopping-cart"></i>
                        <p>NGUYÊN VẬT LIỆU</p>
                    </div>
                    <router-link to="/dictionary-material" @click="hideCategory" class="menu-item items-center">
                        <div class="item-node items-center">
                            <div class="line-1"></div>
                            <div class="line-2"></div>
                        </div>
                        <div class="item-content">Nguyên vật liệu</div>
                    </router-link>
                    <router-link to="/dictionary-categorymaterial" @click="hideCategory" class="menu-item items-center">
                        <div class="item-node items-center">
                            <div class="line-1"></div>
                            <div class="line-2"></div>
                        </div>
                        <div class="item-content">Nhóm nguyên vật liệu</div>
                    </router-link>
                    <router-link to="/dictionary-stock" @click="hideCategory" class="menu-item items-center">
                        <div class="item-node items-center">
                            <div class="line-1"></div>
                            <div class="line-2"></div>
                        </div>
                        <div class="item-content">Kho</div>
                    </router-link>
                    <router-link to="/dictionary-unit" @click="hideCategory" class="menu-item flex">
                        <div class="item-node">
                            <div class="line-1" style="height: 15px;"></div>
                            <div class="line-2" style="margin-left: 13px;"></div>
                        </div>
                        <div class="item-content">Đơn vị tính</div>
                    </router-link>
                </div>
                <div class="menu flex-c">
                    <div class="menu-title items-center">
                        <i class="fab fa-squarespace"></i>
                        <p>KHÁC</p>
                    </div>
                    <div class="menu-item items-center">
                        <div class="item-node items-center">
                            <div class="line-1"></div>
                            <div class="line-2"></div>
                        </div>
                        <div class="item-content">Ca làm việc</div>
                    </div>
                    <div class="menu-item items-center">
                        <div class="item-node items-center">
                            <div class="line-1"></div>
                            <div class="line-2"></div>
                        </div>
                        <div class="item-content">Khung giờ phục vụ</div>
                    </div>
                    <div class="menu-item items-center">
                        <div class="item-node items-center">
                            <div class="line-1"></div>
                            <div class="line-2"></div>
                        </div>
                        <div class="item-content">Két đựng tiền</div>
                    </div>
                    <div class="menu-item items-center">
                        <div class="item-node items-center">
                            <div class="line-1"></div>
                            <div class="line-2"></div>
                        </div>
                        <div class="item-content">Bếp/Bar</div>
                    </div>
                    <div class="menu-item items-center">
                        <div class="item-node items-center">
                            <div class="line-1"></div>
                            <div class="line-2"></div>
                        </div>
                        <div class="item-content">Khu vực chế biến</div>
                    </div>
                    <div class="menu-item items-center">
                        <div class="item-node items-center">
                            <div class="line-1"></div>
                            <div class="line-2"></div>
                        </div>
                        <div class="item-content">Lý do hủy món</div>
                    </div>
                    <div class="menu-item items-center">
                        <div class="item-node items-center">
                            <div class="line-1"></div>
                            <div class="line-2"></div>
                        </div>
                        <div class="item-content">Lý do hủy order</div>
                    </div>
                    <div class="menu-item items-center">
                        <div class="item-node items-center">
                            <div class="line-1"></div>
                            <div class="line-2"></div>
                        </div>
                        <div class="item-content">Mục thu/chi</div>
                    </div>
                    <div class="menu-item items-center">
                        <div class="item-node items-center">
                            <div class="line-1"></div>
                            <div class="line-2"></div>
                        </div>
                        <div class="item-content">Thẻ ngân hàng/ví điện tử</div>
                    </div>
                    <div class="menu-item flex">
                        <div class="item-node">
                            <div class="line-1" style="height: 15px;"></div>
                            <div class="line-2" style="margin-left: 13px;"></div>
                        </div>
                        <div class="item-content">Tài khoản NH/Ví điện tử</div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
export default {
    name: "MainContent",
    props: {

    },
    data() {
        return {
            isCloseSideBar: false,
            isShowCategory: false
        }
    },
    methods: {
        clickOutside() {
            if (this.isShowCategory == true) {
                this.isShowCategory = false;
            }
            // console.log(this.isShowCategory);
        },
        hideCategory() {
            this.isShowCategory = false;
        }
    }
}
</script>
<style scoped>
#main-content {
    width: calc(100% - 230px);
    background-color: #fff;
    padding: 8px;
    position: relative;
}

.full-width-content {
    width: 100% !important;
}

#catergory-mask {
    width: auto;
    height: 100%;
    padding: 20px 0;
    background-color: #fff;
    box-shadow: 4px 0 6px #bbb;
    position: absolute;
    top: 0;
    left: 0;
    z-index: 100;
}

.category-col {
    width: 280px;
    height: fit-content;
    padding: 0 20px;
}

.category-col .menu {
    min-height: 150px;
    margin-top: 8px;
}

.menu .menu-title i {
    font-size: 26px;
    color: #0072bb;
    margin-right: 8px;
}

.menu .menu-title p {
    font-size: 16px;
    line-height: 32px;
    color: #0072bb;
    font-weight: 600;
}

.menu .menu-item {
    color: black;
    text-decoration: none;
}

.menu-item .item-node .line-1 {
    height: 30px;
    border-style: solid;
    border-color: #dadada;
    border-width: 0 0 0 1px;
    margin-left: 13px;
}

.menu-item .item-node .line-2 {
    width: 20px;
    border-style: solid;
    border-color: #dadada;
    border-width: 1px 0 0;
}

.menu-item .item-content {
    border: 1px solid #fff;
    padding: 4px;
    margin-left: 4px;
    cursor: pointer;
}

.menu-item .item-content:hover {
    border: 1px solid #0071c1;
    background-color: #eaf5fd;
}
</style>