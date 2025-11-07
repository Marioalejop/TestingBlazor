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
                bat '''
                    mkdir TestResults || echo Carpeta existente
                    dotnet test --configuration Release --no-restore --logger "trx;LogFileName=TestResults/tests.trx"
                '''
            }
            post {
                always {
                    echo 'Publicando resultados de pruebas...'
                    mstest testResultsFile: 'TestResults/tests.trx'
                }
            }
        }

        stage('Publish Artifacts') {
            when {
                expression { currentBuild.currentResult == 'SUCCESS' }
            }
            steps {
                echo 'Publicando artefactos...'
                bat 'dotnet publish MiApp.Blazor/MiApp.Blazor.csproj -c Release -o publish'
                archiveArtifacts artifacts: 'publish/**', followSymlinks: false
            }
        }
    }

    post {
        success {
            echo '✅ Pipeline finalizado correctamente.'
        }
        failure {
            echo '❌ Pipeline fallido.'
        }
    }
}
