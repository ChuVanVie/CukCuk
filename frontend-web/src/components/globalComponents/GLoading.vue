<template>
    <div class="wrapper-loadding flex-c">
        <div class="icon-loading"></div>
        <div>
            Đang lấy dữ liệu
        </div>
    </div>
</template>
<script>
export default {
    name: "GLoading",
};
</script>
<style scoped>
.wrapper-loadding {
    position: absolute;
    /* position: fixed; */
    top: 0;
    right: 0;
    bottom: 0;
    left: 0;
    z-index: 999;
    background-color: rgba(230, 230, 230);
    display: flex;
    justify-content: center;
    align-items: center;
}

.wrapper-loadding .icon-loading {
    background: url(@/assets/icon/loading.svg) no-repeat center;
    height: 40px;
    width: 40px;
    animation: loading linear 0.8s infinite;
}

@keyframes loading {
    0% {
        transform: rotate(0deg);
    }

    50% {
        transform: rotate(180deg);
    }

    100% {
        transform: rotate(360deg);
    }
}
</style>