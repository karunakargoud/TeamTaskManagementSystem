import { useEffect, useState } from "react";
import { getDashboard } from "../services/dashboardService";

function Dashboard() {

    const [dashboard, setDashboard] =
        useState(null);

    useEffect(() => {

        loadDashboard();

    }, []);


    const loadDashboard = async () => {

        try {

            const data =
                await getDashboard();

            setDashboard(data);

        } catch (error) {

            console.error(error);

        }
    };


    if (!dashboard) {

        return (
            <div className="page">
                <h2>Loading Dashboard...</h2>
            </div>
        );
    }


    return (

        <div className="page">

            <h2>Dashboard</h2>

            <div className="dashboard-grid">

                <div className="card">

                    <h3>Total Tasks</h3>

                    <p>
                        {dashboard.totalTasks}
                    </p>

                </div>


                <div className="card">

                    <h3>To Do</h3>

                    <p>
                        {dashboard.toDo}
                    </p>

                </div>


                <div className="card">

                    <h3>In Progress</h3>

                    <p>
                        {dashboard.inProgress}
                    </p>

                </div>


                <div className="card">

                    <h3>Completed</h3>

                    <p>
                        {dashboard.done}
                    </p>

                </div>


                <div className="card">

                    <h3>High Priority</h3>

                    <p>
                        {dashboard.highPriority}
                    </p>

                </div>


                <div className="card">

                    <h3>Overdue</h3>

                    <p>
                        {dashboard.overdue}
                    </p>

                </div>

            </div>

        </div>
    );
}

export default Dashboard;