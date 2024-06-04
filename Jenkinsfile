pipeline {
  agent any
  stages {
    stage("verify tooling") {
      steps {
        sh '''
          docker version
          docker info
          docker-compose version
        '''
      }
    }
    stage('Prune Docker data') {
      steps {

      }
    }
    stage('Start container') {
      steps {
        sh 'docker-compose down'
        sh 'docker-compose up -d'
        sh 'docker-compose ps'
      }
    }
  }
}
