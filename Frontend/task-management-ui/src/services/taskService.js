import api from "./api";

export const getTasks = async () => {

    const response =
        await api.get("/WorkItems");

    return response.data;
};


export const getTaskById = async (id) => {

    const response =
        await api.get(`/WorkItems/${id}`);

    return response.data;
};


export const createTask = async (task) => {

    const response =
        await api.post("/WorkItems", task);

    return response.data;
};


export const updateTask = async (id, task) => {

    const response =
        await api.put(
            `/WorkItems/${id}`,
            task
        );

    return response.data;
};


export const deleteTask = async (id) => {

    const response =
        await api.delete(
            `/WorkItems/${id}`
        );

    return response.data;
};