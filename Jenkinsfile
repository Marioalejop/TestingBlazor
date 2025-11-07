pipeline {
    agent any

    environment {
        DOTNET_SKIP_FIRST_TIME_EXPERIENCE = 'true'
        DOTNET_CLI_TELEMETRY_OPTOUT = 'true'
        PATH = "${PATH};C:\\Program Files\\dotnet\\"
    }

    stages {
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
                bat 'mkdir TestResults || echo Carpeta existente'
                // Ejecuta pruebas y genera archivo TRX legible por Jenkins
                bat 'dotnet test --no-build --configuration Release --logger "trx;LogFileName=tests.trx" --results-directory TestResults'
            }
            post {
                always {
                    echo 'Publicando resultados de pruebas...'
                    // Publica los resultados .trx
                    mstest testResultsFile: 'TestResults/tests.trx'
                }
            }
        }

        stage('Publish Artifacts') {
            when {
                expression { currentBuild.currentResult == 'SUCCESS' }
            }
            steps {
                echo 'Publicando artefactos compilados...'
                archiveArtifacts artifacts: '**/bin/Release/**/*.dll', fingerprint: true
            }
        }
    }

    post {
        success {
            echo 'Pipeline completado exitosamente.'
        }
        failure {
            echo 'Pipeline fallido.'
        }
        always {
            echo 'Pipeline finalizado.'
        }
    }
}
