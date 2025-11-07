pipeline {
    agent any

    stages {
        stage('Checkout') {
            steps {
                echo 'Clonando repositorio...'
                git branch: 'main', url: 'https://github.com/Marioalejop/TestingBlazor'
            }
        }

        stage('Restore') {
            steps {
                echo 'Restaurando dependencias...'
                bat 'dotnet restore'
            }
        }

        stage('Build') {
            steps {
                echo 'Compilando proyecto...'
                bat 'dotnet build --configuration Release --no-restore'
            }
        }

        stage('Test') {
            steps {
                echo 'Ejecutando pruebas unitarias...'
                bat 'dotnet test --configuration Release --no-restore --logger "trx;LogFileName=tests.trx" --results-directory "TestResults"'
            }
        }

        stage('Publish Results') {
            steps {
                echo 'Publicando resultados de pruebas...'
                mstest testResultsFile: '**/TestResults/*.trx', allowMissing: true, keepLongStdio: true, healthScaleFactor: 1.0
            }
        }
    }

    post {
        success {
            echo '✅ Pipeline completado con éxito.'
        }
        failure {
            echo '❌ Pipeline fallido.'
        }
        always {
            echo '🔁 Limpieza finalizada.'
            cleanWs()
        }
    }
}
