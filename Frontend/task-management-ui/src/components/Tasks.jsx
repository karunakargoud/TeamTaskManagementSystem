import { useEffect, useState } from "react";

import {
    getTasks,
    createTask,
    deleteTask
} from "../services/taskService";

function Tasks() {

    const [tasks, setTasks] =
        useState([]);

    const [task, setTask] =
        useState({
            title: "",
            description: "",
            status: 1,
            priority: "Medium",
            dueDate: "",
            assignedToUserId: "",
            teamId: ""
        });


    useEffect(() => {

        loadTasks();

    }, []);


    const loadTasks = async () => {

        try {

            const data =
                await getTasks();

            setTasks(data);

        } catch (error) {

            console.error(error);

        }
    };


    const handleChange = (e) => {

        setTask({
            ...task,
            [e.target.name]:
                e.target.value
        });
    };


    const handleCreate = async (e) => {

        e.preventDefault();

        try {

            const newTask = {

                title: task.title,

                description:
                    task.description,

                status:
                    Number(task.status),

                priority:
                    task.priority,

                dueDate:
                    task.dueDate || null,

                assignedToUserId:
                    task.assignedToUserId
                        ? Number(
                            task.assignedToUserId
                        )
                        : null,

                teamId:
                    task.teamId
                        ? Number(task.teamId)
                        : null
            };


            await createTask(newTask);

            alert(
                "Task created successfully"
            );

            setTask({
                title: "",
                description: "",
                status: 1,
                priority: "Medium",
                dueDate: "",
                assignedToUserId: "",
                teamId: ""
            });

            loadTasks();

        } catch (error) {

            console.error(error);

            alert("Task creation failed");
        }
    };


    const handleDelete = async (id) => {

        if (!window.confirm(
            "Delete this task?"
        )) {
            return;
        }

        try {

            await deleteTask(id);

            loadTasks();

        } catch (error) {

            console.error(error);

            alert(
                "Unable to delete task"
            );
        }
    };


    const getStatusName = (status) => {

        if (status === 1)
            return "To Do";

        if (status === 2)
            return "In Progress";

        if (status === 3)
            return "Done";

        return "Unknown";
    };


    return (

        <div className="page">

            <h2>Task Management</h2>


            <form
                className="form-card"
                onSubmit={handleCreate}
            >

                <h3>Create Task</h3>


                <input
                    name="title"
                    placeholder="Task Title"
                    value={task.title}
                    onChange={handleChange}
                    required
                />


                <textarea
                    name="description"
                    placeholder="Description"
                    value={task.description}
                    onChange={handleChange}
                />


                <select
                    name="status"
                    value={task.status}
                    onChange={handleChange}
                >

                    <option value={1}>
                        To Do
                    </option>

                    <option value={2}>
                        In Progress
                    </option>

                    <option value={3}>
                        Done
                    </option>

                </select>


                <select
                    name="priority"
                    value={task.priority}
                    onChange={handleChange}
                >

                    <option value="Low">
                        Low
                    </option>

                    <option value="Medium">
                        Medium
                    </option>

                    <option value="High">
                        High
                    </option>

                </select>


                <label>
                    Due Date
                </label>

                <input
                    type="date"
                    name="dueDate"
                    value={task.dueDate}
                    onChange={handleChange}
                />


                <input
                    type="number"
                    name="assignedToUserId"
                    placeholder="Assigned User ID"
                    value={
                        task.assignedToUserId
                    }
                    onChange={handleChange}
                />


                <input
                    type="number"
                    name="teamId"
                    placeholder="Team ID"
                    value={task.teamId}
                    onChange={handleChange}
                />


                <button type="submit">
                    Create Task
                </button>

            </form>


            <h3>Task List</h3>


            <table>

                <thead>

                    <tr>

                        <th>ID</th>
                        <th>Title</th>
                        <th>Status</th>
                        <th>Priority</th>
                        <th>Assigned User</th>
                        <th>Team</th>
                        <th>Action</th>

                    </tr>

                </thead>


                <tbody>

                    {tasks.map((item) => (

                        <tr
                            key={
                                item.workItemId
                            }
                        >

                            <td>
                                {item.workItemId}
                            </td>

                            <td>
                                {item.title}
                            </td>

                            <td>
                                {getStatusName(
                                    item.status
                                )}
                            </td>

                            <td>
                                {item.priority}
                            </td>

                            <td>
                                {
                                    item.assignedToUserId
                                }
                            </td>

                            <td>
                                {item.teamId}
                            </td>

                            <td>

                                <button
                                    className="delete-btn"
                                    onClick={() =>
                                        handleDelete(
                                            item.workItemId
                                        )
                                    }
                                >
                                    Delete
                                </button>

                            </td>

                        </tr>

                    ))}

                </tbody>

            </table>

        </div>
    );
}

export default Tasks;