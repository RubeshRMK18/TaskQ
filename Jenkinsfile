pipeline {
    agent any

    environment {
        BACKEND_IMAGE  = 'rubeshmageshkumar/taskq-backend'
        FRONTEND_IMAGE = 'rubeshmageshkumar/taskq-frontend'
    }

    stages {

        stage('Checkout Code') {
            steps {
                echo 'Pulling code from GitHub...'
                checkout scm
            }
        }

        stage('Build Backend Image') {
            steps {
                echo 'Building backend...'
                bat 'docker build -t %BACKEND_IMAGE%:latest ./TaskQ_webapi --no-cache'
            }
        }

        stage('Build Frontend Image') {
            steps {
                echo 'Building frontend...'
                bat 'docker build -t %FRONTEND_IMAGE%:latest ./TaskQ --no-cache'
            }
        }

        stage('Push to Docker Hub') {
            steps {
                echo 'Pushing to Docker Hub...'
                withCredentials([usernamePassword(
                    credentialsId: 'dockerhub-credentials',
                    usernameVariable: 'DOCKER_USER',
                    passwordVariable: 'DOCKER_PASS'
                )]) {
                    bat 'docker login -u %DOCKER_USER% -p %DOCKER_PASS%'
                    bat 'docker push %BACKEND_IMAGE%:latest'
                    bat 'docker push %FRONTEND_IMAGE%:latest'
                }
            }
        }

        stage('Deploy to Azure') {
            steps {
                echo 'Restarting Azure Web Apps...'
                bat 'az webapp restart --name taskqbackend --resource-group TaskQ'
                bat 'az webapp restart --name taskqfrontend --resource-group TaskQ'
            }
        }
    }

    post {
        success {
            echo 'Deployed successfully!'
        }
        failure {
            echo 'Build failed!'
        }
    }
}