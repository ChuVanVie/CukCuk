import baseAPI from "@/resources/baseAPI";
import axios from "axios"

export default class BaseService {
    constructor(controller) {
        this.controller = controller;
    }

    getAll() {
        try {
            return axios.get(baseAPI + this.controller);
        } catch (error) {
            console.log(error);
        }
    }

    getPaging() {

    }

    post(obj) {
        try {
            return axios.post(baseAPI + this.controller, obj);
        } catch (error) {
            console.log(error);
        }
    }

    put(id, obj) {
        try {
            return axios.put(baseAPI + this.controller + "/" + id, obj);

        } catch (error) {
            console.log(error);
        }
    }

    delete(id) {
        try {
            return axios.delete(baseAPI + this.controller + "/" + id);
        } catch (error) {
            console.log(error);
        }
    }

}