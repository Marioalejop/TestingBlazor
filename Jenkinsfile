pipeline {
    agent any

    environment {
        CONFIGURATION = 'Release'
        TEST_RESULTS = 'TestResults'
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
                bat "dotnet build --configuration %CONFIGURATION% --no-restore"
            }
        }

        stage('Test') {
            steps {
                echo 'Ejecutando pruebas unitarias...'
                bat "mkdir %TEST_RESULTS%"
                bat "dotnet test --no-build --configuration %CONFIGURATION% --logger \"trx;LogFileName=tests.trx\" --results-directory %TEST_RESULTS%"
            }
            post {
                always {
                    echo 'Publicando resultados de pruebas...'
                    junit allowEmptyResults: true, testResults: '**/TestResults/*.trx'
                }
                failure {
                    echo 'Las pruebas fallaron.'
                }
                success {
                    echo 'Todas las pruebas pasaron correctamente.'
                }
            }
        }

        stage('Publish Artifacts') {
            when {
                branch 'main'
            }
            steps {
                echo 'Publicando artefactos...'
                bat "dotnet publish --configuration %CONFIGURATION% -o output"
                archiveArtifacts artifacts: 'output/**/*.*', fingerprint: true
            }
        }
    }

    post {
        always {
            echo 'Pipeline finalizado.'
        }
    }
}
